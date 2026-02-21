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
    using UnityEngine;
    public partial class XTween_Controller
    {
        /// <summary>
        /// 动画 - 位置方法
        /// </summary>
        private void TweenPlay_Position()
        {
            if (TweenTypes != XTweenTypes.位置_Position)
                return;

            if (TweenTypes_Positions == XTweenTypes_Positions.锚点位置_AnchoredPosition)
            {
                CurrentTweener = XTween.xt_AnchoredPosition_To(Target_RectTransform, EndValue_Vector2, Duration, IsRelative, IsAutoKill, EaseMode, IsFromMode, () => FromValue_Vector2, UseCurve, Curve).SetLoop(LoopCount, LoopType).SetLoopingDelay(LoopDelay).SetDelay(Delay).OnStart(() =>
                {
                    if (act_on_start != null)
                        act_on_start();
                }).OnStop(() =>
                {
                    if (act_on_stop != null)
                        act_on_stop();
                }).OnDelayUpdate((progress) =>
                {
                    if (act_on_delayUpdate != null)
                        act_on_delayUpdate(progress);
                }).OnUpdate<Vector2>((value, linearProgress, time) =>
                {
                    if (act_onUpdate_vector2 != null)
                        act_onUpdate_vector2(value, linearProgress, time);
                }).OnStepUpdate<Vector2>((value, linearProgress, elapsedTime) =>
                {
                    if (act_onStepUpdate_vector2 != null)
                        act_onStepUpdate_vector2(value, linearProgress, elapsedTime);
                }).OnProgress<Vector2>((value, linearProgress) =>
                {
                    if (act_onProgress_vector2 != null)
                        act_onProgress_vector2(value, linearProgress);
                }).OnKill(() =>
                {
                    if (act_on_kill != null)
                        act_on_kill();
                }).OnPause(() =>
                {
                    if (act_on_pause != null)
                        act_on_pause();
                }).OnResume(() =>
                {
                    if (act_on_resume != null)
                        act_on_resume();
                }).OnRewind(() =>
                {
                    if (act_on_rewind != null)
                        act_on_rewind();
                }).OnComplete((duration) =>
                {
                    if (act_on_complete != null)
                        act_on_complete(duration);
                });
            }
            else if (TweenTypes_Positions == XTweenTypes_Positions.锚点位置3D_AnchoredPosition3D)
            {
                CurrentTweener = XTween.xt_AnchoredPosition3D_To(Target_RectTransform, EndValue_Vector3, Duration, IsRelative, IsAutoKill, EaseMode, IsFromMode, () => FromValue_Vector3, UseCurve, Curve).SetLoop(LoopCount, LoopType).SetLoopingDelay(LoopDelay).SetDelay(Delay).OnStart(() =>
                {
                    if (act_on_start != null)
                        act_on_start();
                }).OnStop(() =>
                {
                    if (act_on_stop != null)
                        act_on_stop();
                }).OnDelayUpdate((progress) =>
                {
                    if (act_on_delayUpdate != null)
                        act_on_delayUpdate(progress);
                }).OnUpdate<Vector3>((value, linearProgress, time) =>
                {
                    if (act_onUpdate_vector3 != null)
                        act_onUpdate_vector3(value, linearProgress, time);
                }).OnStepUpdate<Vector3>((value, linearProgress, elapsedTime) =>
                {
                    if (act_onStepUpdate_vector3 != null)
                        act_onStepUpdate_vector3(value, linearProgress, elapsedTime);
                }).OnProgress<Vector3>((value, linearProgress) =>
                {
                    if (act_onProgress_vector3 != null)
                        act_onProgress_vector3(value, linearProgress);
                }).OnKill(() =>
                {
                    if (act_on_kill != null)
                        act_on_kill();
                }).OnPause(() =>
                {
                    if (act_on_pause != null)
                        act_on_pause();
                }).OnResume(() =>
                {
                    if (act_on_resume != null)
                        act_on_resume();
                }).OnRewind(() =>
                {
                    if (act_on_rewind != null)
                        act_on_rewind();
                }).OnComplete((duration) =>
                {
                    if (act_on_complete != null)
                        act_on_complete(duration);
                });
            }
        }
    }
}
