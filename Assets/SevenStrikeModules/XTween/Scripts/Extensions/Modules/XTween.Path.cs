/*
 * ============================================================================
 * ⚠️ 版权声明（禁止删除、禁止修改、衍生作品必须保留此注释）⚠️
 * ============================================================================
 * 版权声明 Copyright (C) 2025-Present Nanjing SevenStrike Media Co., Ltd.
 * 中文名称：南京塞维斯传媒有限公司
 * 英文名称：SevenStrikeMedia
 * 项目作者：徐寅智
 * 项目名称：XTween - Unity 高性能动画架构插件
 * 项目启动：2025年8月
 * 官方网站：http://sevenstrike.com/
 * 授权协议：GNU Affero General Public License Version 3 (AGPL 3.0)
 * 协议说明：
 * 1. 你可以自由使用、修改、分发本插件的源代码，但必须保留此版权注释
 * 2. 基于本插件修改后的衍生作品，必须同样遵循 AGPL 3.0 授权协议
 * 3. 若将本插件用于网络服务（如云端Unity编辑器、在线动效生成工具），必须公开修改后的完整源代码
 * 4. 完整协议文本可查阅：https://www.gnu.org/licenses/agpl-3.0.html
 * ============================================================================
 * 违反本注释保留要求，将违反 AGPL 3.0 授权协议，需承担相应法律责任
 */
namespace SevenStrikeModules.XTween
{
    using System;
    using UnityEngine;

    public static partial class XTween
    {
        /// <summary>
        /// 创建一个沿路径移动的动画，支持路径点、路径类型等功能。
        /// 该方法允许RectTransform对象沿着预定义的路径点进行平滑移动，并可设置路径方向、旋转行为等参数。
        /// </summary>
        /// <param name="rectTransform">要进行路径移动的RectTransform组件</param>
        /// <param name="TweenPath">路径工具对象，包含路径点和路径类型等信息</param>
        /// <param name="duration">动画持续时间（秒）</param>
        /// <param name="pathOrientation">路径方向类型，决定对象在移动过程中的旋转行为</param>
        /// <param name="pathOrientationVector">路径方向向量，指定对象旋转时对齐的轴向</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_PathMove(this UnityEngine.RectTransform rectTransform, XTween_PathTool TweenPath, float duration, XTween_PathOrientation pathOrientation = XTween_PathOrientation.无, XTween_PathOrientationVector pathOrientationVector = XTween_PathOrientationVector.正向X轴, bool autokill = false)
        {
            if (rectTransform == null)
            {
                throw new ArgumentNullException("rectTransform");
            }

            if (TweenPath.PathPoints == null || TweenPath.PathPoints.Count < 2)
            {
                throw new ArgumentException("Need at least 2 path points");
            }

            Vector3 startValue = rectTransform.anchoredPosition3D;

            Vector3[] processedPath = TweenPath.GetProcessedPathPoints();
            XTween_PathLengthData lengthData = TweenPath.CalculatePathLength(processedPath);
            // 在tweener创建前添加
            Quaternion initialRotation = rectTransform.rotation;

            if (Application.isPlaying)
            {
                var tweener = XTween_Pool.CreateTween<XTween_Specialized_Vector3>();

                tweener.Initialize(startValue, processedPath[^1], duration * XTween_Dashboard.DurationMultiply);

                tweener.OnStart(() =>
                {
                    TweenPath.IsWorldMode = true;
                    TweenPath.SaveStartPosition();

                    if (TweenPath.act_on_pathStart != null)
                        TweenPath.act_on_pathStart(startValue);
                })
               .OnUpdate((pos, linearProgress, time) =>
               {
                   // 计算缓动进度
                   float easedProgress = tweener.CalculateEasedProgress(linearProgress);

                   // YOYO模式特殊处理
                   if (tweener.LoopType == XTween_LoopType.Yoyo && tweener.IsReversing)
                   {
                       easedProgress = 1f - easedProgress;
                   }

                   // 计算路径进度
                   float pathProgress = Mathf.Clamp01(easedProgress * TweenPath.PathLimitePercent);

                   // 获取路径位置
                   Vector3 currentPos = TweenPath.GetPositionOnPath(processedPath, lengthData, pathProgress);
                   rectTransform.anchoredPosition3D = currentPos;
                   TweenPath.PathProgress = pathProgress;

                   // 传递路径运动位置
                   if (TweenPath.act_on_pathMove != null)
                       TweenPath.act_on_pathMove(currentPos);

                   // 传递路径运动进度值
                   if (TweenPath.act_on_pathProgress != null)
                       TweenPath.act_on_pathProgress(pathProgress);

                   // === 新增：旋转控制（支持任意轴）===
                   if (pathOrientation != XTween_PathOrientation.无)
                   {
                       Vector3 lookDirection = Vector3.zero;
                       Vector3 worldPosition = rectTransform.position;

                       switch (pathOrientation)
                       {
                           case XTween_PathOrientation.跟随路径:
                               // 计算路径切线方向
                               lookDirection = TweenPath.GetTangentOnPath(processedPath, lengthData, TweenPath.PathProgress);
                               // 根据指定轴设置旋转
                               if (lookDirection != Vector3.zero)
                               {
                                   Quaternion targetRotation;

                                   switch (pathOrientationVector)
                                   {
                                       case XTween_PathOrientationVector.正向X轴:
                                           // 使+X轴指向目标方向
                                           targetRotation = Quaternion.LookRotation(lookDirection, Vector3.forward) * Quaternion.Euler(-90, -90, 0);
                                           break;
                                       case XTween_PathOrientationVector.正向Y轴:
                                           // 使+Y轴指向目标方向
                                           targetRotation = Quaternion.LookRotation(lookDirection, Vector3.forward) * Quaternion.Euler(270, 0, 180);
                                           break;
                                       case XTween_PathOrientationVector.正向Z轴:
                                           // 使+Z轴指向目标方向
                                           targetRotation = Quaternion.LookRotation(lookDirection, Vector3.forward) * Quaternion.Euler(0, 0, 180);
                                           break;
                                       case XTween_PathOrientationVector.反向X轴:
                                           // 使-X轴指向目标方向
                                           targetRotation = Quaternion.LookRotation(lookDirection, Vector3.forward) * Quaternion.Euler(90, 90, 0);
                                           break;
                                       case XTween_PathOrientationVector.反向Y轴:
                                           // 使-Y轴指向目标方向
                                           targetRotation = Quaternion.LookRotation(lookDirection, Vector3.forward) * Quaternion.Euler(-90, 0, 0);
                                           break;
                                       case XTween_PathOrientationVector.反向Z轴:
                                           // 使-Z轴指向目标方向
                                           targetRotation = Quaternion.LookRotation(lookDirection, Vector3.forward) * Quaternion.Euler(180, 0, 0);
                                           break;
                                       default:
                                           // 默认使X轴指向目标方向
                                           targetRotation = Quaternion.LookRotation(lookDirection, Vector3.forward) * Quaternion.Euler(90, -90, 0);
                                           break;
                                   }

                                   rectTransform.rotation = targetRotation;

                                   // 传递路径运动旋转朝向
                                   if (TweenPath.act_on_pathOrientation != null)
                                       TweenPath.act_on_pathOrientation(targetRotation);
                               }
                               break;
                           case XTween_PathOrientation.注视目标物体:
                               if (TweenPath.LookAtObject != null)
                               {
                                   // 使用世界坐标计算方向
                                   lookDirection = TweenPath.LookAtObject.transform.position - worldPosition;

                                   // 根据指定轴设置旋转
                                   if (lookDirection != Vector3.zero)
                                   {
                                       // 直接设置transform的轴向，更直观准确
                                       switch (pathOrientationVector)
                                       {
                                           case XTween_PathOrientationVector.正向X轴:
                                               rectTransform.right = lookDirection.normalized;
                                               break;

                                           case XTween_PathOrientationVector.正向Y轴:
                                               rectTransform.up = lookDirection.normalized;
                                               break;

                                           case XTween_PathOrientationVector.正向Z轴:
                                               rectTransform.forward = lookDirection.normalized;
                                               break;

                                           case XTween_PathOrientationVector.反向X轴:
                                               rectTransform.right = -lookDirection.normalized;
                                               break;

                                           case XTween_PathOrientationVector.反向Y轴:
                                               rectTransform.up = -lookDirection.normalized;
                                               break;

                                           case XTween_PathOrientationVector.反向Z轴:
                                               rectTransform.forward = -lookDirection.normalized;
                                               break;
                                       }

                                       // 传递路径运动旋转朝向
                                       if (TweenPath.act_on_pathLookatOrientation_withObject != null)
                                           TweenPath.act_on_pathLookatOrientation_withObject(rectTransform.localEulerAngles);
                                   }
                               }
                               break;
                           case XTween_PathOrientation.注视目标位置:
                               // 使用世界坐标计算方向
                               lookDirection = TweenPath.LookAtPosition - worldPosition;

                               // 根据指定轴设置旋转
                               if (lookDirection != Vector3.zero)
                               {
                                   // 直接设置transform的轴向，更直观准确
                                   switch (pathOrientationVector)
                                   {
                                       case XTween_PathOrientationVector.正向X轴:
                                           rectTransform.right = lookDirection.normalized;
                                           break;

                                       case XTween_PathOrientationVector.正向Y轴:
                                           rectTransform.up = lookDirection.normalized;
                                           break;

                                       case XTween_PathOrientationVector.正向Z轴:
                                           rectTransform.forward = lookDirection.normalized;
                                           break;

                                       case XTween_PathOrientationVector.反向X轴:
                                           rectTransform.right = -lookDirection.normalized;
                                           break;

                                       case XTween_PathOrientationVector.反向Y轴:
                                           rectTransform.up = -lookDirection.normalized;
                                           break;

                                       case XTween_PathOrientationVector.反向Z轴:
                                           rectTransform.forward = -lookDirection.normalized;
                                           break;
                                   }

                                   // 传递路径运动旋转朝向
                                   if (TweenPath.act_on_pathLookatOrientation_withPosition != null)
                                       TweenPath.act_on_pathLookatOrientation_withPosition(rectTransform.localEulerAngles);
                               }
                               break;
                       }
                   }
               })
               .OnComplete((duration) =>
               {
                   TweenPath.IsWorldMode = true;
                   float finalProgress;

                   // YOYO模式特殊处理
                   if (tweener.LoopType == XTween_LoopType.Yoyo)
                   {
                       // 根据总循环次数的奇偶性确定终点位置
                       finalProgress = (tweener.CurrentLoop % 2 == 1) ? 0f : TweenPath.PathLimitePercent;
                   }
                   else
                   {
                       finalProgress = TweenPath.PathLimitePercent;
                   }

                   Vector3 currentPos = TweenPath.GetPositionOnPath(processedPath, lengthData, finalProgress);
                   rectTransform.anchoredPosition3D = currentPos;
                   TweenPath.PathProgress = finalProgress;

                   if (TweenPath.act_on_pathComplete != null)
                       TweenPath.act_on_pathComplete(currentPos);
               })
               .OnRewind(() =>
               {
                   TweenPath.IsWorldMode = true;
                   TweenPath.RestoreStartPosition();
                   TweenPath.PathProgress = 0;
                   rectTransform.rotation = initialRotation;
               })
               .OnKill(() =>
               {
                   TweenPath.IsWorldMode = false;
                   TweenPath.RestoreStartPosition();
                   TweenPath.PathProgress = 0;
                   rectTransform.rotation = initialRotation;
               })
               .SetAutokill(autokill)
               .SetRelative(false);
                return tweener;
            }
            else
            {
                XTween_Interface tweener = null;
                tweener = new XTween_Specialized_Vector3(startValue, processedPath[^1], duration * XTween_Dashboard.DurationMultiply)
                    .OnStart(() =>
                    {
                        TweenPath.IsWorldMode = true;
                        TweenPath.SaveStartPosition();

                        if (TweenPath.act_on_pathStart != null)
                            TweenPath.act_on_pathStart(startValue);
                    })
                    .OnUpdate((pos, linearProgress, time) =>
                    {
                        // 计算缓动进度
                        float easedProgress;
                        if (tweener.UseCustomEaseCurve && tweener.CustomEaseCurve != null)
                        {
                            easedProgress = tweener.CustomEaseCurve.Evaluate(linearProgress);
                        }
                        else
                        {
                            easedProgress = XTween_EaseCache.Evaluate(tweener.EaseMode, linearProgress);
                        }

                        // YOYO模式特殊处理
                        if (tweener.LoopType == XTween_LoopType.Yoyo && tweener.IsReversing)
                        {
                            easedProgress = 1f - easedProgress;
                        }

                        TweenPath.PathProgress = Mathf.Clamp01(easedProgress * TweenPath.PathLimitePercent);

                        Vector3 currentPos = TweenPath.GetPositionOnPath(processedPath, lengthData, TweenPath.PathProgress);
                        rectTransform.anchoredPosition3D = currentPos;

                        // 传递路径运动位置
                        if (TweenPath.act_on_pathMove != null)
                            TweenPath.act_on_pathMove(currentPos);

                        // 传递路径运动进度值
                        if (TweenPath.act_on_pathProgress != null)
                            TweenPath.act_on_pathProgress(TweenPath.PathProgress);

                        // === 新增：旋转控制（支持任意轴）===
                        if (pathOrientation != XTween_PathOrientation.无)
                        {
                            Vector3 lookDirection = Vector3.zero;
                            Vector3 worldPosition = rectTransform.position;

                            switch (pathOrientation)
                            {
                                case XTween_PathOrientation.跟随路径:
                                    // 计算路径切线方向
                                    lookDirection = TweenPath.GetTangentOnPath(processedPath, lengthData, TweenPath.PathProgress);
                                    // 根据指定轴设置旋转
                                    if (lookDirection != Vector3.zero)
                                    {
                                        Quaternion targetRotation;

                                        switch (pathOrientationVector)
                                        {
                                            case XTween_PathOrientationVector.正向X轴:
                                                // 使+X轴指向目标方向
                                                targetRotation = Quaternion.LookRotation(lookDirection, Vector3.forward) * Quaternion.Euler(-90, -90, 0);
                                                break;
                                            case XTween_PathOrientationVector.正向Y轴:
                                                // 使+Y轴指向目标方向
                                                targetRotation = Quaternion.LookRotation(lookDirection, Vector3.forward) * Quaternion.Euler(270, 0, 180);
                                                break;
                                            case XTween_PathOrientationVector.正向Z轴:
                                                // 使+Z轴指向目标方向
                                                targetRotation = Quaternion.LookRotation(lookDirection, Vector3.forward) * Quaternion.Euler(0, 0, 180);
                                                break;
                                            case XTween_PathOrientationVector.反向X轴:
                                                // 使-X轴指向目标方向
                                                targetRotation = Quaternion.LookRotation(lookDirection, Vector3.forward) * Quaternion.Euler(90, 90, 0);
                                                break;
                                            case XTween_PathOrientationVector.反向Y轴:
                                                // 使-Y轴指向目标方向
                                                targetRotation = Quaternion.LookRotation(lookDirection, Vector3.forward) * Quaternion.Euler(-90, 0, 0);
                                                break;
                                            case XTween_PathOrientationVector.反向Z轴:
                                                // 使-Z轴指向目标方向
                                                targetRotation = Quaternion.LookRotation(lookDirection, Vector3.forward) * Quaternion.Euler(180, 0, 0);
                                                break;
                                            default:
                                                // 默认使X轴指向目标方向
                                                targetRotation = Quaternion.LookRotation(lookDirection, Vector3.forward) * Quaternion.Euler(90, -90, 0);
                                                break;
                                        }

                                        rectTransform.rotation = targetRotation;

                                        // 传递路径运动旋转朝向
                                        if (TweenPath.act_on_pathOrientation != null)
                                            TweenPath.act_on_pathOrientation(targetRotation);
                                    }
                                    break;
                                case XTween_PathOrientation.注视目标物体:
                                    if (TweenPath.LookAtObject != null)
                                    {
                                        // 使用世界坐标计算方向
                                        lookDirection = TweenPath.LookAtObject.transform.position - worldPosition;

                                        // 根据指定轴设置旋转
                                        if (lookDirection != Vector3.zero)
                                        {
                                            // 直接设置transform的轴向，更直观准确
                                            switch (pathOrientationVector)
                                            {
                                                case XTween_PathOrientationVector.正向X轴:
                                                    rectTransform.right = lookDirection.normalized;
                                                    break;

                                                case XTween_PathOrientationVector.正向Y轴:
                                                    rectTransform.up = lookDirection.normalized;
                                                    break;

                                                case XTween_PathOrientationVector.正向Z轴:
                                                    rectTransform.forward = lookDirection.normalized;
                                                    break;

                                                case XTween_PathOrientationVector.反向X轴:
                                                    rectTransform.right = -lookDirection.normalized;
                                                    break;

                                                case XTween_PathOrientationVector.反向Y轴:
                                                    rectTransform.up = -lookDirection.normalized;
                                                    break;

                                                case XTween_PathOrientationVector.反向Z轴:
                                                    rectTransform.forward = -lookDirection.normalized;
                                                    break;
                                            }

                                            // 传递路径运动旋转朝向
                                            if (TweenPath.act_on_pathLookatOrientation_withObject != null)
                                                TweenPath.act_on_pathLookatOrientation_withObject(rectTransform.localEulerAngles);
                                        }
                                    }
                                    break;
                                case XTween_PathOrientation.注视目标位置:
                                    // 使用世界坐标计算方向
                                    lookDirection = TweenPath.LookAtPosition - worldPosition;

                                    // 根据指定轴设置旋转
                                    if (lookDirection != Vector3.zero)
                                    {
                                        // 直接设置transform的轴向，更直观准确
                                        switch (pathOrientationVector)
                                        {
                                            case XTween_PathOrientationVector.正向X轴:
                                                rectTransform.right = lookDirection.normalized;
                                                break;

                                            case XTween_PathOrientationVector.正向Y轴:
                                                rectTransform.up = lookDirection.normalized;
                                                break;

                                            case XTween_PathOrientationVector.正向Z轴:
                                                rectTransform.forward = lookDirection.normalized;
                                                break;

                                            case XTween_PathOrientationVector.反向X轴:
                                                rectTransform.right = -lookDirection.normalized;
                                                break;

                                            case XTween_PathOrientationVector.反向Y轴:
                                                rectTransform.up = -lookDirection.normalized;
                                                break;

                                            case XTween_PathOrientationVector.反向Z轴:
                                                rectTransform.forward = -lookDirection.normalized;
                                                break;
                                        }

                                        // 传递路径运动旋转朝向
                                        if (TweenPath.act_on_pathLookatOrientation_withPosition != null)
                                            TweenPath.act_on_pathLookatOrientation_withPosition(rectTransform.localEulerAngles);
                                    }
                                    break;
                            }
                        }
                    })
                    .OnComplete((duration) =>
                    {
                        TweenPath.IsWorldMode = true;
                        float finalProgress;

                        // YOYO模式特殊处理
                        if (tweener.LoopType == XTween_LoopType.Yoyo)
                        {
                            // 根据总循环次数的奇偶性确定终点位置
                            finalProgress = (tweener.CurrentLoop % 2 == 1) ? 0f : TweenPath.PathLimitePercent;
                        }
                        else
                        {
                            finalProgress = TweenPath.PathLimitePercent;
                        }

                        Vector3 currentPos = TweenPath.GetPositionOnPath(processedPath, lengthData, finalProgress);
                        rectTransform.anchoredPosition3D = currentPos;
                        TweenPath.PathProgress = finalProgress;

                        if (TweenPath.act_on_pathComplete != null)
                            TweenPath.act_on_pathComplete(currentPos);
                    })
                    .OnRewind(() =>
                    {
                        TweenPath.IsWorldMode = true;
                        TweenPath.RestoreStartPosition();
                        TweenPath.PathProgress = 0;
                        rectTransform.rotation = initialRotation;
                    })
                    .OnKill(() =>
                    {
                        TweenPath.IsWorldMode = false;
                        TweenPath.RestoreStartPosition();
                        TweenPath.PathProgress = 0;
                        rectTransform.rotation = initialRotation;
                    })
                    .SetAutokill(false)
                    .SetRelative(false);
                return tweener;
            }
        }
        /// <summary>
        /// 创建一个沿路径移动的动画，支持路径点、路径类型等功能。
        /// 该方法允许RectTransform对象沿着预定义的路径点进行平滑移动，并可设置路径方向、旋转行为等参数。
        /// </summary>
        /// <param name="rectTransform">要进行路径移动的RectTransform组件</param>
        /// <param name="TweenPath">路径工具对象，包含路径点和路径类型等信息</param>
        /// <param name="duration">动画持续时间（秒）</param>
        /// <param name="pathOrientation">路径方向类型，决定对象在移动过程中的旋转行为</param>
        /// <param name="pathOrientationVector">路径方向向量，指定对象旋转时对齐的轴向</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_PathMove(this UnityEngine.RectTransform rectTransform, XTween_PathTool TweenPath, float duration, XTween_PathOrientation pathOrientation = XTween_PathOrientation.无, XTween_PathOrientationVector pathOrientationVector = XTween_PathOrientationVector.正向X轴, bool autokill = false, EaseMode easeMode = EaseMode.InOutCubic, bool useCurve = false, AnimationCurve curve = null)
        {
            if (rectTransform == null)
            {
                throw new ArgumentNullException("rectTransform");
            }

            if (TweenPath.PathPoints == null || TweenPath.PathPoints.Count < 2)
            {
                throw new ArgumentException("Need at least 2 path points");
            }

            Vector3 startValue = rectTransform.anchoredPosition3D;

            Vector3[] processedPath = TweenPath.GetProcessedPathPoints();
            XTween_PathLengthData lengthData = TweenPath.CalculatePathLength(processedPath);
            // 在tweener创建前添加
            Quaternion initialRotation = rectTransform.rotation;

            if (Application.isPlaying)
            {
                var tweener = XTween_Pool.CreateTween<XTween_Specialized_Vector3>();

                tweener.Initialize(startValue, processedPath[^1], duration * XTween_Dashboard.DurationMultiply);

                if (useCurve)
                {
                    tweener.OnStart(() =>
                    {
                        TweenPath.IsWorldMode = true;
                        TweenPath.SaveStartPosition();
                        if (TweenPath.act_on_pathStart != null)
                            TweenPath.act_on_pathStart(startValue);
                    })
                        .OnUpdate((pos, linearProgress, time) =>
                        {
                            // 计算缓动进度
                            float easedProgress = tweener.CalculateEasedProgress(linearProgress);

                            // YOYO模式特殊处理
                            if (tweener.LoopType == XTween_LoopType.Yoyo && tweener.IsReversing)
                            {
                                easedProgress = 1f - easedProgress;
                            }

                            // 计算路径进度
                            float pathProgress = Mathf.Clamp01(easedProgress * TweenPath.PathLimitePercent);

                            // 获取路径位置
                            Vector3 currentPos = TweenPath.GetPositionOnPath(processedPath, lengthData, pathProgress);
                            rectTransform.anchoredPosition3D = currentPos;
                            TweenPath.PathProgress = pathProgress;

                            // 传递路径运动位置
                            if (TweenPath.act_on_pathMove != null)
                                TweenPath.act_on_pathMove(currentPos);

                            // 传递路径运动进度值
                            if (TweenPath.act_on_pathProgress != null)
                                TweenPath.act_on_pathProgress(pathProgress);

                            // === 新增：旋转控制（支持任意轴）===
                            if (pathOrientation != XTween_PathOrientation.无)
                            {
                                Vector3 lookDirection = Vector3.zero;
                                Vector3 worldPosition = rectTransform.position;

                                switch (pathOrientation)
                                {
                                    case XTween_PathOrientation.跟随路径:
                                        // 计算路径切线方向
                                        lookDirection = TweenPath.GetTangentOnPath(processedPath, lengthData, TweenPath.PathProgress);
                                        // 根据指定轴设置旋转
                                        if (lookDirection != Vector3.zero)
                                        {
                                            Quaternion targetRotation;

                                            switch (pathOrientationVector)
                                            {
                                                case XTween_PathOrientationVector.正向X轴:
                                                    // 使+X轴指向目标方向
                                                    targetRotation = Quaternion.LookRotation(lookDirection, Vector3.forward) * Quaternion.Euler(-90, -90, 0);
                                                    break;
                                                case XTween_PathOrientationVector.正向Y轴:
                                                    // 使+Y轴指向目标方向
                                                    targetRotation = Quaternion.LookRotation(lookDirection, Vector3.forward) * Quaternion.Euler(270, 0, 180);
                                                    break;
                                                case XTween_PathOrientationVector.正向Z轴:
                                                    // 使+Z轴指向目标方向
                                                    targetRotation = Quaternion.LookRotation(lookDirection, Vector3.forward) * Quaternion.Euler(0, 0, 180);
                                                    break;
                                                case XTween_PathOrientationVector.反向X轴:
                                                    // 使-X轴指向目标方向
                                                    targetRotation = Quaternion.LookRotation(lookDirection, Vector3.forward) * Quaternion.Euler(90, 90, 0);
                                                    break;
                                                case XTween_PathOrientationVector.反向Y轴:
                                                    // 使-Y轴指向目标方向
                                                    targetRotation = Quaternion.LookRotation(lookDirection, Vector3.forward) * Quaternion.Euler(-90, 0, 0);
                                                    break;
                                                case XTween_PathOrientationVector.反向Z轴:
                                                    // 使-Z轴指向目标方向
                                                    targetRotation = Quaternion.LookRotation(lookDirection, Vector3.forward) * Quaternion.Euler(180, 0, 0);
                                                    break;
                                                default:
                                                    // 默认使X轴指向目标方向
                                                    targetRotation = Quaternion.LookRotation(lookDirection, Vector3.forward) * Quaternion.Euler(90, -90, 0);
                                                    break;
                                            }

                                            rectTransform.rotation = targetRotation;

                                            // 传递路径运动旋转朝向
                                            if (TweenPath.act_on_pathOrientation != null)
                                                TweenPath.act_on_pathOrientation(targetRotation);
                                        }
                                        break;
                                    case XTween_PathOrientation.注视目标物体:
                                        if (TweenPath.LookAtObject != null)
                                        {
                                            // 使用世界坐标计算方向
                                            lookDirection = TweenPath.LookAtObject.transform.position - worldPosition;

                                            // 根据指定轴设置旋转
                                            if (lookDirection != Vector3.zero)
                                            {
                                                // 直接设置transform的轴向，更直观准确
                                                switch (pathOrientationVector)
                                                {
                                                    case XTween_PathOrientationVector.正向X轴:
                                                        rectTransform.right = lookDirection.normalized;
                                                        break;

                                                    case XTween_PathOrientationVector.正向Y轴:
                                                        rectTransform.up = lookDirection.normalized;
                                                        break;

                                                    case XTween_PathOrientationVector.正向Z轴:
                                                        rectTransform.forward = lookDirection.normalized;
                                                        break;

                                                    case XTween_PathOrientationVector.反向X轴:
                                                        rectTransform.right = -lookDirection.normalized;
                                                        break;

                                                    case XTween_PathOrientationVector.反向Y轴:
                                                        rectTransform.up = -lookDirection.normalized;
                                                        break;

                                                    case XTween_PathOrientationVector.反向Z轴:
                                                        rectTransform.forward = -lookDirection.normalized;
                                                        break;
                                                }
                                                // 传递路径运动旋转朝向
                                                if (TweenPath.act_on_pathLookatOrientation_withObject != null)
                                                    TweenPath.act_on_pathLookatOrientation_withObject(rectTransform.localEulerAngles);
                                            }
                                        }
                                        break;
                                    case XTween_PathOrientation.注视目标位置:
                                        // 使用世界坐标计算方向
                                        lookDirection = TweenPath.LookAtPosition - worldPosition;

                                        // 根据指定轴设置旋转
                                        if (lookDirection != Vector3.zero)
                                        {
                                            // 直接设置transform的轴向，更直观准确
                                            switch (pathOrientationVector)
                                            {
                                                case XTween_PathOrientationVector.正向X轴:
                                                    rectTransform.right = lookDirection.normalized;
                                                    break;

                                                case XTween_PathOrientationVector.正向Y轴:
                                                    rectTransform.up = lookDirection.normalized;
                                                    break;

                                                case XTween_PathOrientationVector.正向Z轴:
                                                    rectTransform.forward = lookDirection.normalized;
                                                    break;

                                                case XTween_PathOrientationVector.反向X轴:
                                                    rectTransform.right = -lookDirection.normalized;
                                                    break;

                                                case XTween_PathOrientationVector.反向Y轴:
                                                    rectTransform.up = -lookDirection.normalized;
                                                    break;

                                                case XTween_PathOrientationVector.反向Z轴:
                                                    rectTransform.forward = -lookDirection.normalized;
                                                    break;
                                            }

                                            // 传递路径运动旋转朝向
                                            if (TweenPath.act_on_pathLookatOrientation_withPosition != null)
                                                TweenPath.act_on_pathLookatOrientation_withPosition(rectTransform.localEulerAngles);
                                        }
                                        break;
                                }
                            }
                        })
                        .OnComplete((duration) =>
                        {
                            TweenPath.IsWorldMode = true;
                            float finalProgress;

                            // YOYO模式特殊处理
                            if (tweener.LoopType == XTween_LoopType.Yoyo)
                            {
                                // 根据总循环次数的奇偶性确定终点位置
                                finalProgress = (tweener.CurrentLoop % 2 == 1) ? 0f : TweenPath.PathLimitePercent;
                            }
                            else
                            {
                                finalProgress = TweenPath.PathLimitePercent;
                            }

                            Vector3 currentPos = TweenPath.GetPositionOnPath(processedPath, lengthData, finalProgress);
                            rectTransform.anchoredPosition3D = currentPos;
                            TweenPath.PathProgress = finalProgress;

                            if (TweenPath.act_on_pathComplete != null)
                                TweenPath.act_on_pathComplete(currentPos);
                        })
                        .OnRewind(() =>
                        {
                            TweenPath.IsWorldMode = true;
                            TweenPath.RestoreStartPosition();
                            TweenPath.PathProgress = 0;
                            rectTransform.rotation = initialRotation;
                        })
                        .OnKill(() =>
                        {
                            TweenPath.IsWorldMode = false;
                            TweenPath.RestoreStartPosition();
                            TweenPath.PathProgress = 0;
                            rectTransform.rotation = initialRotation;
                        })
                        .SetEase(curve)
                        .SetAutokill(autokill)
                        .SetRelative(false);
                }
                else
                {
                    tweener.OnStart(() =>
                    {
                        TweenPath.IsWorldMode = true;
                        TweenPath.SaveStartPosition();
                        if (TweenPath.act_on_pathStart != null)
                            TweenPath.act_on_pathStart(startValue);
                    })
                        .OnUpdate((pos, linearProgress, time) =>
                        {
                            // 计算缓动进度
                            float easedProgress = tweener.CalculateEasedProgress(linearProgress);

                            // YOYO模式特殊处理
                            if (tweener.LoopType == XTween_LoopType.Yoyo && tweener.IsReversing)
                            {
                                easedProgress = 1f - easedProgress;
                            }

                            // 计算路径进度
                            float pathProgress = Mathf.Clamp01(easedProgress * TweenPath.PathLimitePercent);

                            // 获取路径位置
                            Vector3 currentPos = TweenPath.GetPositionOnPath(processedPath, lengthData, pathProgress);
                            rectTransform.anchoredPosition3D = currentPos;
                            TweenPath.PathProgress = pathProgress;

                            // 传递路径运动位置
                            if (TweenPath.act_on_pathMove != null)
                                TweenPath.act_on_pathMove(currentPos);

                            // 传递路径运动进度值
                            if (TweenPath.act_on_pathProgress != null)
                                TweenPath.act_on_pathProgress(pathProgress);

                            // === 新增：旋转控制（支持任意轴）===
                            if (pathOrientation != XTween_PathOrientation.无)
                            {
                                Vector3 lookDirection = Vector3.zero;
                                Vector3 worldPosition = rectTransform.position;

                                switch (pathOrientation)
                                {
                                    case XTween_PathOrientation.跟随路径:
                                        // 计算路径切线方向
                                        lookDirection = TweenPath.GetTangentOnPath(processedPath, lengthData, TweenPath.PathProgress);
                                        // 根据指定轴设置旋转
                                        if (lookDirection != Vector3.zero)
                                        {
                                            Quaternion targetRotation;

                                            switch (pathOrientationVector)
                                            {
                                                case XTween_PathOrientationVector.正向X轴:
                                                    // 使+X轴指向目标方向
                                                    targetRotation = Quaternion.LookRotation(lookDirection, Vector3.forward) * Quaternion.Euler(-90, -90, 0);
                                                    break;
                                                case XTween_PathOrientationVector.正向Y轴:
                                                    // 使+Y轴指向目标方向
                                                    targetRotation = Quaternion.LookRotation(lookDirection, Vector3.forward) * Quaternion.Euler(270, 0, 180);
                                                    break;
                                                case XTween_PathOrientationVector.正向Z轴:
                                                    // 使+Z轴指向目标方向
                                                    targetRotation = Quaternion.LookRotation(lookDirection, Vector3.forward) * Quaternion.Euler(0, 0, 180);
                                                    break;
                                                case XTween_PathOrientationVector.反向X轴:
                                                    // 使-X轴指向目标方向
                                                    targetRotation = Quaternion.LookRotation(lookDirection, Vector3.forward) * Quaternion.Euler(90, 90, 0);
                                                    break;
                                                case XTween_PathOrientationVector.反向Y轴:
                                                    // 使-Y轴指向目标方向
                                                    targetRotation = Quaternion.LookRotation(lookDirection, Vector3.forward) * Quaternion.Euler(-90, 0, 0);
                                                    break;
                                                case XTween_PathOrientationVector.反向Z轴:
                                                    // 使-Z轴指向目标方向
                                                    targetRotation = Quaternion.LookRotation(lookDirection, Vector3.forward) * Quaternion.Euler(180, 0, 0);
                                                    break;
                                                default:
                                                    // 默认使X轴指向目标方向
                                                    targetRotation = Quaternion.LookRotation(lookDirection, Vector3.forward) * Quaternion.Euler(90, -90, 0);
                                                    break;
                                            }

                                            rectTransform.rotation = targetRotation;

                                            // 传递路径运动旋转朝向
                                            if (TweenPath.act_on_pathOrientation != null)
                                                TweenPath.act_on_pathOrientation(targetRotation);
                                        }
                                        break;
                                    case XTween_PathOrientation.注视目标物体:
                                        if (TweenPath.LookAtObject != null)
                                        {
                                            // 使用世界坐标计算方向
                                            lookDirection = TweenPath.LookAtObject.transform.position - worldPosition;

                                            // 根据指定轴设置旋转
                                            if (lookDirection != Vector3.zero)
                                            {
                                                // 直接设置transform的轴向，更直观准确
                                                switch (pathOrientationVector)
                                                {
                                                    case XTween_PathOrientationVector.正向X轴:
                                                        rectTransform.right = lookDirection.normalized;
                                                        break;

                                                    case XTween_PathOrientationVector.正向Y轴:
                                                        rectTransform.up = lookDirection.normalized;
                                                        break;

                                                    case XTween_PathOrientationVector.正向Z轴:
                                                        rectTransform.forward = lookDirection.normalized;
                                                        break;

                                                    case XTween_PathOrientationVector.反向X轴:
                                                        rectTransform.right = -lookDirection.normalized;
                                                        break;

                                                    case XTween_PathOrientationVector.反向Y轴:
                                                        rectTransform.up = -lookDirection.normalized;
                                                        break;

                                                    case XTween_PathOrientationVector.反向Z轴:
                                                        rectTransform.forward = -lookDirection.normalized;
                                                        break;
                                                }
                                                // 传递路径运动旋转朝向
                                                if (TweenPath.act_on_pathLookatOrientation_withObject != null)
                                                    TweenPath.act_on_pathLookatOrientation_withObject(rectTransform.localEulerAngles);
                                            }
                                        }
                                        break;
                                    case XTween_PathOrientation.注视目标位置:
                                        // 使用世界坐标计算方向
                                        lookDirection = TweenPath.LookAtPosition - worldPosition;

                                        // 根据指定轴设置旋转
                                        if (lookDirection != Vector3.zero)
                                        {
                                            // 直接设置transform的轴向，更直观准确
                                            switch (pathOrientationVector)
                                            {
                                                case XTween_PathOrientationVector.正向X轴:
                                                    rectTransform.right = lookDirection.normalized;
                                                    break;

                                                case XTween_PathOrientationVector.正向Y轴:
                                                    rectTransform.up = lookDirection.normalized;
                                                    break;

                                                case XTween_PathOrientationVector.正向Z轴:
                                                    rectTransform.forward = lookDirection.normalized;
                                                    break;

                                                case XTween_PathOrientationVector.反向X轴:
                                                    rectTransform.right = -lookDirection.normalized;
                                                    break;

                                                case XTween_PathOrientationVector.反向Y轴:
                                                    rectTransform.up = -lookDirection.normalized;
                                                    break;

                                                case XTween_PathOrientationVector.反向Z轴:
                                                    rectTransform.forward = -lookDirection.normalized;
                                                    break;
                                            }

                                            // 传递路径运动旋转朝向
                                            if (TweenPath.act_on_pathLookatOrientation_withPosition != null)
                                                TweenPath.act_on_pathLookatOrientation_withPosition(rectTransform.localEulerAngles);
                                        }
                                        break;
                                }
                            }
                        })
                        .OnComplete((duration) =>
                        {
                            TweenPath.IsWorldMode = true;
                            float finalProgress;

                            // YOYO模式特殊处理
                            if (tweener.LoopType == XTween_LoopType.Yoyo)
                            {
                                // 根据总循环次数的奇偶性确定终点位置
                                finalProgress = (tweener.CurrentLoop % 2 == 1) ? 0f : TweenPath.PathLimitePercent;
                            }
                            else
                            {
                                finalProgress = TweenPath.PathLimitePercent;
                            }

                            Vector3 currentPos = TweenPath.GetPositionOnPath(processedPath, lengthData, finalProgress);
                            rectTransform.anchoredPosition3D = currentPos;
                            TweenPath.PathProgress = finalProgress;

                            if (TweenPath.act_on_pathComplete != null)
                                TweenPath.act_on_pathComplete(currentPos);
                        })
                        .OnRewind(() =>
                        {
                            TweenPath.IsWorldMode = true;
                            TweenPath.RestoreStartPosition();
                            TweenPath.PathProgress = 0;
                            rectTransform.rotation = initialRotation;
                        })
                        .OnKill(() =>
                        {
                            TweenPath.IsWorldMode = false;
                            TweenPath.RestoreStartPosition();
                            TweenPath.PathProgress = 0;
                            rectTransform.rotation = initialRotation;
                        })
                        .SetEase(easeMode)
                        .SetAutokill(autokill)
                        .SetRelative(false);
                }
                return tweener;
            }
            else
            {
                XTween_Interface tweener = null;
                if (useCurve)
                {
                    tweener = new XTween_Specialized_Vector3(startValue, processedPath[^1], duration * XTween_Dashboard.DurationMultiply)
                    .OnStart(() =>
                    {
                        TweenPath.IsWorldMode = true;
                        TweenPath.SaveStartPosition();
                        if (TweenPath.act_on_pathStart != null)
                            TweenPath.act_on_pathStart(startValue);
                    })
                    .OnUpdate((pos, linearProgress, time) =>
                    {
                        // 计算缓动进度
                        float easedProgress;
                        if (tweener.UseCustomEaseCurve && tweener.CustomEaseCurve != null)
                        {
                            easedProgress = tweener.CustomEaseCurve.Evaluate(linearProgress);
                        }
                        else
                        {
                            easedProgress = XTween_EaseCache.Evaluate(tweener.EaseMode, linearProgress);
                        }

                        // YOYO模式特殊处理
                        if (tweener.LoopType == XTween_LoopType.Yoyo && tweener.IsReversing)
                        {
                            easedProgress = 1f - easedProgress;
                        }

                        TweenPath.PathProgress = Mathf.Clamp01(easedProgress * TweenPath.PathLimitePercent);

                        Vector3 currentPos = TweenPath.GetPositionOnPath(processedPath, lengthData, TweenPath.PathProgress);
                        rectTransform.anchoredPosition3D = currentPos;

                        // 传递路径运动位置
                        if (TweenPath.act_on_pathMove != null)
                            TweenPath.act_on_pathMove(currentPos);

                        // 传递路径运动进度值
                        if (TweenPath.act_on_pathProgress != null)
                            TweenPath.act_on_pathProgress(TweenPath.PathProgress);

                        // === 新增：旋转控制（支持任意轴）===
                        if (pathOrientation != XTween_PathOrientation.无)
                        {
                            Vector3 lookDirection = Vector3.zero;
                            Vector3 worldPosition = rectTransform.position;

                            switch (pathOrientation)
                            {
                                case XTween_PathOrientation.跟随路径:
                                    // 计算路径切线方向
                                    lookDirection = TweenPath.GetTangentOnPath(processedPath, lengthData, TweenPath.PathProgress);
                                    // 根据指定轴设置旋转
                                    if (lookDirection != Vector3.zero)
                                    {
                                        Quaternion targetRotation;

                                        switch (pathOrientationVector)
                                        {
                                            case XTween_PathOrientationVector.正向X轴:
                                                // 使+X轴指向目标方向
                                                targetRotation = Quaternion.LookRotation(lookDirection, Vector3.forward) * Quaternion.Euler(-90, -90, 0);
                                                break;
                                            case XTween_PathOrientationVector.正向Y轴:
                                                // 使+Y轴指向目标方向
                                                targetRotation = Quaternion.LookRotation(lookDirection, Vector3.forward) * Quaternion.Euler(270, 0, 180);
                                                break;
                                            case XTween_PathOrientationVector.正向Z轴:
                                                // 使+Z轴指向目标方向
                                                targetRotation = Quaternion.LookRotation(lookDirection, Vector3.forward) * Quaternion.Euler(0, 0, 180);
                                                break;
                                            case XTween_PathOrientationVector.反向X轴:
                                                // 使-X轴指向目标方向
                                                targetRotation = Quaternion.LookRotation(lookDirection, Vector3.forward) * Quaternion.Euler(90, 90, 0);
                                                break;
                                            case XTween_PathOrientationVector.反向Y轴:
                                                // 使-Y轴指向目标方向
                                                targetRotation = Quaternion.LookRotation(lookDirection, Vector3.forward) * Quaternion.Euler(-90, 0, 0);
                                                break;
                                            case XTween_PathOrientationVector.反向Z轴:
                                                // 使-Z轴指向目标方向
                                                targetRotation = Quaternion.LookRotation(lookDirection, Vector3.forward) * Quaternion.Euler(180, 0, 0);
                                                break;
                                            default:
                                                // 默认使X轴指向目标方向
                                                targetRotation = Quaternion.LookRotation(lookDirection, Vector3.forward) * Quaternion.Euler(90, -90, 0);
                                                break;
                                        }

                                        rectTransform.rotation = targetRotation;

                                        // 传递路径运动旋转朝向
                                        if (TweenPath.act_on_pathOrientation != null)
                                            TweenPath.act_on_pathOrientation(targetRotation);
                                    }
                                    break;
                                case XTween_PathOrientation.注视目标物体:
                                    if (TweenPath.LookAtObject != null)
                                    {
                                        // 使用世界坐标计算方向
                                        lookDirection = TweenPath.LookAtObject.transform.position - worldPosition;

                                        // 根据指定轴设置旋转
                                        if (lookDirection != Vector3.zero)
                                        {
                                            // 直接设置transform的轴向，更直观准确
                                            switch (pathOrientationVector)
                                            {
                                                case XTween_PathOrientationVector.正向X轴:
                                                    rectTransform.right = lookDirection.normalized;
                                                    break;

                                                case XTween_PathOrientationVector.正向Y轴:
                                                    rectTransform.up = lookDirection.normalized;
                                                    break;

                                                case XTween_PathOrientationVector.正向Z轴:
                                                    rectTransform.forward = lookDirection.normalized;
                                                    break;

                                                case XTween_PathOrientationVector.反向X轴:
                                                    rectTransform.right = -lookDirection.normalized;
                                                    break;

                                                case XTween_PathOrientationVector.反向Y轴:
                                                    rectTransform.up = -lookDirection.normalized;
                                                    break;

                                                case XTween_PathOrientationVector.反向Z轴:
                                                    rectTransform.forward = -lookDirection.normalized;
                                                    break;
                                            }

                                            // 传递路径运动旋转朝向
                                            if (TweenPath.act_on_pathLookatOrientation_withObject != null)
                                                TweenPath.act_on_pathLookatOrientation_withObject(rectTransform.localEulerAngles);
                                        }
                                    }
                                    break;
                                case XTween_PathOrientation.注视目标位置:
                                    // 使用世界坐标计算方向
                                    lookDirection = TweenPath.LookAtPosition - worldPosition;

                                    // 根据指定轴设置旋转
                                    if (lookDirection != Vector3.zero)
                                    {
                                        // 直接设置transform的轴向，更直观准确
                                        switch (pathOrientationVector)
                                        {
                                            case XTween_PathOrientationVector.正向X轴:
                                                rectTransform.right = lookDirection.normalized;
                                                break;

                                            case XTween_PathOrientationVector.正向Y轴:
                                                rectTransform.up = lookDirection.normalized;
                                                break;

                                            case XTween_PathOrientationVector.正向Z轴:
                                                rectTransform.forward = lookDirection.normalized;
                                                break;

                                            case XTween_PathOrientationVector.反向X轴:
                                                rectTransform.right = -lookDirection.normalized;
                                                break;

                                            case XTween_PathOrientationVector.反向Y轴:
                                                rectTransform.up = -lookDirection.normalized;
                                                break;

                                            case XTween_PathOrientationVector.反向Z轴:
                                                rectTransform.forward = -lookDirection.normalized;
                                                break;
                                        }

                                        // 传递路径运动旋转朝向
                                        if (TweenPath.act_on_pathLookatOrientation_withPosition != null)
                                            TweenPath.act_on_pathLookatOrientation_withPosition(rectTransform.localEulerAngles);
                                    }
                                    break;
                            }
                        }
                    })
                    .OnComplete((duration) =>
                    {
                        TweenPath.IsWorldMode = true;
                        float finalProgress;

                        // YOYO模式特殊处理
                        if (tweener.LoopType == XTween_LoopType.Yoyo)
                        {
                            // 根据总循环次数的奇偶性确定终点位置
                            finalProgress = (tweener.CurrentLoop % 2 == 1) ? 0f : TweenPath.PathLimitePercent;
                        }
                        else
                        {
                            finalProgress = TweenPath.PathLimitePercent;
                        }

                        Vector3 currentPos = TweenPath.GetPositionOnPath(processedPath, lengthData, finalProgress);
                        rectTransform.anchoredPosition3D = currentPos;
                        TweenPath.PathProgress = finalProgress;

                        if (TweenPath.act_on_pathComplete != null)
                            TweenPath.act_on_pathComplete(currentPos);
                    })
                    .OnRewind(() =>
                    {
                        TweenPath.IsWorldMode = true;
                        TweenPath.RestoreStartPosition();
                        TweenPath.PathProgress = 0;
                        rectTransform.rotation = initialRotation;
                    })
                    .OnKill(() =>
                    {
                        TweenPath.IsWorldMode = false;
                        TweenPath.RestoreStartPosition();
                        TweenPath.PathProgress = 0;
                        rectTransform.rotation = initialRotation;
                    })
                    .SetEase(curve)
                    .SetAutokill(false)
                    .SetRelative(false);
                }
                else
                {
                    tweener = new XTween_Specialized_Vector3(startValue, processedPath[^1], duration * XTween_Dashboard.DurationMultiply)
                    .OnStart(() =>
                    {
                        TweenPath.IsWorldMode = true;
                        TweenPath.SaveStartPosition();
                        if (TweenPath.act_on_pathStart != null)
                            TweenPath.act_on_pathStart(startValue);
                    })
                    .OnUpdate((pos, linearProgress, time) =>
                    {
                        // 计算缓动进度
                        float easedProgress;
                        if (tweener.UseCustomEaseCurve && tweener.CustomEaseCurve != null)
                        {
                            easedProgress = tweener.CustomEaseCurve.Evaluate(linearProgress);
                        }
                        else
                        {
                            easedProgress = XTween_EaseCache.Evaluate(tweener.EaseMode, linearProgress);
                        }

                        // YOYO模式特殊处理
                        if (tweener.LoopType == XTween_LoopType.Yoyo && tweener.IsReversing)
                        {
                            easedProgress = 1f - easedProgress;
                        }

                        TweenPath.PathProgress = Mathf.Clamp01(easedProgress * TweenPath.PathLimitePercent);

                        Vector3 currentPos = TweenPath.GetPositionOnPath(processedPath, lengthData, TweenPath.PathProgress);
                        rectTransform.anchoredPosition3D = currentPos;

                        // 传递路径运动位置
                        if (TweenPath.act_on_pathMove != null)
                            TweenPath.act_on_pathMove(currentPos);

                        // 传递路径运动进度值
                        if (TweenPath.act_on_pathProgress != null)
                            TweenPath.act_on_pathProgress(TweenPath.PathProgress);

                        // === 新增：旋转控制（支持任意轴）===
                        if (pathOrientation != XTween_PathOrientation.无)
                        {
                            Vector3 lookDirection = Vector3.zero;
                            Vector3 worldPosition = rectTransform.position;

                            switch (pathOrientation)
                            {
                                case XTween_PathOrientation.跟随路径:
                                    // 计算路径切线方向
                                    lookDirection = TweenPath.GetTangentOnPath(processedPath, lengthData, TweenPath.PathProgress);
                                    // 根据指定轴设置旋转
                                    if (lookDirection != Vector3.zero)
                                    {
                                        Quaternion targetRotation;

                                        switch (pathOrientationVector)
                                        {
                                            case XTween_PathOrientationVector.正向X轴:
                                                // 使+X轴指向目标方向
                                                targetRotation = Quaternion.LookRotation(lookDirection, Vector3.forward) * Quaternion.Euler(-90, -90, 0);
                                                break;
                                            case XTween_PathOrientationVector.正向Y轴:
                                                // 使+Y轴指向目标方向
                                                targetRotation = Quaternion.LookRotation(lookDirection, Vector3.forward) * Quaternion.Euler(270, 0, 180);
                                                break;
                                            case XTween_PathOrientationVector.正向Z轴:
                                                // 使+Z轴指向目标方向
                                                targetRotation = Quaternion.LookRotation(lookDirection, Vector3.forward) * Quaternion.Euler(0, 0, 180);
                                                break;
                                            case XTween_PathOrientationVector.反向X轴:
                                                // 使-X轴指向目标方向
                                                targetRotation = Quaternion.LookRotation(lookDirection, Vector3.forward) * Quaternion.Euler(90, 90, 0);
                                                break;
                                            case XTween_PathOrientationVector.反向Y轴:
                                                // 使-Y轴指向目标方向
                                                targetRotation = Quaternion.LookRotation(lookDirection, Vector3.forward) * Quaternion.Euler(-90, 0, 0);
                                                break;
                                            case XTween_PathOrientationVector.反向Z轴:
                                                // 使-Z轴指向目标方向
                                                targetRotation = Quaternion.LookRotation(lookDirection, Vector3.forward) * Quaternion.Euler(180, 0, 0);
                                                break;
                                            default:
                                                // 默认使X轴指向目标方向
                                                targetRotation = Quaternion.LookRotation(lookDirection, Vector3.forward) * Quaternion.Euler(90, -90, 0);
                                                break;
                                        }

                                        rectTransform.rotation = targetRotation;

                                        // 传递路径运动旋转朝向
                                        if (TweenPath.act_on_pathOrientation != null)
                                            TweenPath.act_on_pathOrientation(targetRotation);
                                    }
                                    break;
                                case XTween_PathOrientation.注视目标物体:
                                    if (TweenPath.LookAtObject != null)
                                    {
                                        // 使用世界坐标计算方向
                                        lookDirection = TweenPath.LookAtObject.transform.position - worldPosition;

                                        // 根据指定轴设置旋转
                                        if (lookDirection != Vector3.zero)
                                        {
                                            // 直接设置transform的轴向，更直观准确
                                            switch (pathOrientationVector)
                                            {
                                                case XTween_PathOrientationVector.正向X轴:
                                                    rectTransform.right = lookDirection.normalized;
                                                    break;

                                                case XTween_PathOrientationVector.正向Y轴:
                                                    rectTransform.up = lookDirection.normalized;
                                                    break;

                                                case XTween_PathOrientationVector.正向Z轴:
                                                    rectTransform.forward = lookDirection.normalized;
                                                    break;

                                                case XTween_PathOrientationVector.反向X轴:
                                                    rectTransform.right = -lookDirection.normalized;
                                                    break;

                                                case XTween_PathOrientationVector.反向Y轴:
                                                    rectTransform.up = -lookDirection.normalized;
                                                    break;

                                                case XTween_PathOrientationVector.反向Z轴:
                                                    rectTransform.forward = -lookDirection.normalized;
                                                    break;
                                            }

                                            // 传递路径运动旋转朝向
                                            if (TweenPath.act_on_pathLookatOrientation_withObject != null)
                                                TweenPath.act_on_pathLookatOrientation_withObject(rectTransform.localEulerAngles);
                                        }
                                    }
                                    break;
                                case XTween_PathOrientation.注视目标位置:
                                    // 使用世界坐标计算方向
                                    lookDirection = TweenPath.LookAtPosition - worldPosition;

                                    // 根据指定轴设置旋转
                                    if (lookDirection != Vector3.zero)
                                    {
                                        // 直接设置transform的轴向，更直观准确
                                        switch (pathOrientationVector)
                                        {
                                            case XTween_PathOrientationVector.正向X轴:
                                                rectTransform.right = lookDirection.normalized;
                                                break;

                                            case XTween_PathOrientationVector.正向Y轴:
                                                rectTransform.up = lookDirection.normalized;
                                                break;

                                            case XTween_PathOrientationVector.正向Z轴:
                                                rectTransform.forward = lookDirection.normalized;
                                                break;

                                            case XTween_PathOrientationVector.反向X轴:
                                                rectTransform.right = -lookDirection.normalized;
                                                break;

                                            case XTween_PathOrientationVector.反向Y轴:
                                                rectTransform.up = -lookDirection.normalized;
                                                break;

                                            case XTween_PathOrientationVector.反向Z轴:
                                                rectTransform.forward = -lookDirection.normalized;
                                                break;
                                        }

                                        // 传递路径运动旋转朝向
                                        if (TweenPath.act_on_pathLookatOrientation_withPosition != null)
                                            TweenPath.act_on_pathLookatOrientation_withPosition(rectTransform.localEulerAngles);
                                    }
                                    break;
                            }
                        }
                    })
                    .OnComplete((duration) =>
                    {
                        TweenPath.IsWorldMode = true;
                        float finalProgress;

                        // YOYO模式特殊处理
                        if (tweener.LoopType == XTween_LoopType.Yoyo)
                        {
                            // 根据总循环次数的奇偶性确定终点位置
                            finalProgress = (tweener.CurrentLoop % 2 == 1) ? 0f : TweenPath.PathLimitePercent;
                        }
                        else
                        {
                            finalProgress = TweenPath.PathLimitePercent;
                        }

                        Vector3 currentPos = TweenPath.GetPositionOnPath(processedPath, lengthData, finalProgress);
                        rectTransform.anchoredPosition3D = currentPos;
                        TweenPath.PathProgress = finalProgress;

                        if (TweenPath.act_on_pathComplete != null)
                            TweenPath.act_on_pathComplete(currentPos);
                    })
                    .OnRewind(() =>
                    {
                        TweenPath.IsWorldMode = true;
                        TweenPath.RestoreStartPosition();
                        TweenPath.PathProgress = 0;
                        rectTransform.rotation = initialRotation;
                    })
                    .OnKill(() =>
                    {
                        TweenPath.IsWorldMode = false;
                        TweenPath.RestoreStartPosition();
                        TweenPath.PathProgress = 0;
                        rectTransform.rotation = initialRotation;
                    })
                    .SetEase(easeMode)
                    .SetAutokill(false)
                    .SetRelative(false);
                }
                return tweener;
            }
        }
    }
}