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
        /// 动画 - TmpText方法
        /// </summary>
        private void TweenPlay_TmpText()
        {
            if (TweenTypes != XTweenTypes.文字_TmpText)
                return;

            if (TweenTypes_TmpText == XTweenTypes_TmpText.文字尺寸_FontSize)
            {
                CurrentTweener = XTween.xt_FontSize_To(Target_TmpText, EndValue_Float, Duration, IsAutoKill, EaseMode, IsFromMode, () => FromValue_Float, UseCurve, Curve).SetLoop(LoopCount, LoopType).SetLoopingDelay(LoopDelay).SetDelay(Delay).OnStart(() =>
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
                }).OnUpdate<float>((value, linearProgress, time) =>
                {
                    if (act_onUpdate_float != null)
                        act_onUpdate_float(value, linearProgress, time);
                }).OnStepUpdate<float>((value, linearProgress, elapsedTime) =>
                {
                    if (act_onStepUpdate_float != null)
                        act_onStepUpdate_float(value, linearProgress, elapsedTime);
                }).OnProgress<float>((value, linearProgress) =>
                {
                    if (act_onProgress_float != null)
                        act_onProgress_float(value, linearProgress);
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
            else if (TweenTypes_TmpText == XTweenTypes_TmpText.文字行高_LineHeight)
            {
                CurrentTweener = XTween.xt_FontLineHeight_To(Target_TmpText, EndValue_Float, Duration, IsAutoKill, EaseMode, IsFromMode, () => FromValue_Float, UseCurve, Curve).SetLoop(LoopCount, LoopType).SetLoopingDelay(LoopDelay).SetDelay(Delay).OnStart(() =>
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
                }).OnUpdate<float>((value, linearProgress, time) =>
                {
                    if (act_onUpdate_float != null)
                        act_onUpdate_float(value, linearProgress, time);
                }).OnStepUpdate<float>((value, linearProgress, elapsedTime) =>
                {
                    if (act_onStepUpdate_float != null)
                        act_onStepUpdate_float(value, linearProgress, elapsedTime);
                }).OnProgress<float>((value, linearProgress) =>
                {
                    if (act_onProgress_float != null)
                        act_onProgress_float(value, linearProgress);
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
            else if (TweenTypes_TmpText == XTweenTypes_TmpText.文字颜色_Color)
            {
                CurrentTweener = XTween.xt_FontColor_To(Target_TmpText, EndValue_Color, Duration, IsAutoKill, EaseMode, IsFromMode, () => FromValue_Color, UseCurve, Curve).SetLoop(LoopCount, LoopType).SetLoopingDelay(LoopDelay).SetDelay(Delay).OnStart(() =>
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
                }).OnUpdate<Color>((value, linearProgress, time) =>
                {
                    if (act_onUpdate_color != null)
                        act_onUpdate_color(value, linearProgress, time);
                }).OnStepUpdate<Color>((value, linearProgress, elapsedTime) =>
                {
                    if (act_onStepUpdate_color != null)
                        act_onStepUpdate_color(value, linearProgress, elapsedTime);
                }).OnProgress<Color>((value, linearProgress) =>
                {
                    if (act_onProgress_color != null)
                        act_onProgress_color(value, linearProgress);
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
            else if (TweenTypes_TmpText == XTweenTypes_TmpText.文字内容_Content)
            {
                CurrentTweener = XTween.xt_FontText_To(Target_TmpText, IsExtendedString, EndValue_String, Duration, IsAutoKill, EaseMode, IsFromMode, () => FromValue_String, UseCurve, Curve).SetLoop(LoopCount, LoopType).SetLoopingDelay(LoopDelay).SetDelay(Delay).OnStart(() =>
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
                }).OnUpdate<string>((value, linearProgress, time) =>
                {
                    if (act_onUpdate_string != null)
                        act_onUpdate_string(value, linearProgress, time);
                }).OnStepUpdate<string>((value, linearProgress, elapsedTime) =>
                {
                    if (act_onStepUpdate_string != null)
                        act_onStepUpdate_string(value, linearProgress, elapsedTime);
                }).OnProgress<string>((value, linearProgress) =>
                {
                    if (act_onProgress_string != null)
                        act_onProgress_string(value, linearProgress);
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
            else if (TweenTypes_TmpText == XTweenTypes_TmpText.文字间距_Character)
            {
                CurrentTweener = XTween.xt_FontCharacter_To(Target_TmpText, EndValue_Float, Duration, IsAutoKill, EaseMode, IsFromMode, () => FromValue_Float, UseCurve, Curve).SetLoop(LoopCount, LoopType).SetLoopingDelay(LoopDelay).SetDelay(Delay).OnStart(() =>
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
                }).OnUpdate<float>((value, linearProgress, time) =>
                {
                    if (act_onUpdate_float != null)
                        act_onUpdate_float(value, linearProgress, time);
                }).OnStepUpdate<float>((value, linearProgress, elapsedTime) =>
                {
                    if (act_onStepUpdate_float != null)
                        act_onStepUpdate_float(value, linearProgress, elapsedTime);
                }).OnProgress<float>((value, linearProgress) =>
                {
                    if (act_onProgress_float != null)
                        act_onProgress_float(value, linearProgress);
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
            else if (TweenTypes_TmpText == XTweenTypes_TmpText.文字边距_Margin)
            {
                CurrentTweener = XTween.xt_FontMargin_To(Target_TmpText, EndValue_Vector4, Duration, IsAutoKill, EaseMode, IsFromMode, () => FromValue_Vector4, UseCurve, Curve).SetLoop(LoopCount, LoopType).SetLoopingDelay(LoopDelay).SetDelay(Delay).OnStart(() =>
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
                }).OnUpdate<Vector4>((value, linearProgress, time) =>
                {
                    if (act_onUpdate_vector4 != null)
                        act_onUpdate_vector4(value, linearProgress, time);
                }).OnStepUpdate<Vector4>((value, linearProgress, elapsedTime) =>
                {
                    if (act_onStepUpdate_vector4 != null)
                        act_onStepUpdate_vector4(value, linearProgress, elapsedTime);
                }).OnProgress<Vector4>((value, linearProgress) =>
                {
                    if (act_onProgress_vector4 != null)
                        act_onProgress_vector4(value, linearProgress);
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
