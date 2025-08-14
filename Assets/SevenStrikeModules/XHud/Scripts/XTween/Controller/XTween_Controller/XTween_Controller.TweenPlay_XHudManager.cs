namespace SevenStrikeModules.XTween
{
    using UnityEngine;

    public partial class XTween_Controller
    {
        /// <summary>
        /// 动画 - XHud管理器方法
        /// </summary>
        private void TweenPlay_XHudManager()
        {
            if (TweenTypes != XTweenTypes.管理器_XHudManager)
                return;

            if (TweenTypes_XHudManager == XTweenTypes_XHudManager.屏幕空间元素透明度_ScreenElementsOpacity)
            {
                CurrentTweener = XTween.xt_ScreenElementsOpacity_To(XTween_Dashboard.HudManagerGet(), EndValue_Float, Duration, IsAutoKill, EaseMode, IsFromMode, () => FromValue_Float, UseCurve, Curve).SetLoop(LoopCount, LoopType).SetLoopingDelay(LoopDelay).SetDelay(Delay).OnStart(() =>
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
            else if (TweenTypes_XHudManager == XTweenTypes_XHudManager.世界空间元素透明度_WorldElementsOpacity)
            {
                CurrentTweener = XTween.xt_WorldElementsOpacity_To(XTween_Dashboard.HudManagerGet(), EndValue_Float, Duration, IsAutoKill, EaseMode, IsFromMode, () => FromValue_Float, UseCurve, Curve).SetLoop(LoopCount, LoopType).SetLoopingDelay(LoopDelay).SetDelay(Delay).OnStart(() =>
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
            else if (TweenTypes_XHudManager == XTweenTypes_XHudManager.遮罩层透明度_MaskOpacity)
            {
                CurrentTweener = XTween.xt_Mask_Opacity_To(XTween_Dashboard.HudManagerGet(), EndValue_Float, Duration, IsAutoKill, EaseMode, IsFromMode, () => FromValue_Float, UseCurve, Curve).SetLoop(LoopCount, LoopType).SetLoopingDelay(LoopDelay).SetDelay(Delay).OnStart(() =>
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
            else if (TweenTypes_XHudManager == XTweenTypes_XHudManager.遮罩层颜色_MaskColor)
            {
                CurrentTweener = XTween.xt_Mask_Color_To(XTween_Dashboard.HudManagerGet(), EndValue_Color, Duration, IsAutoKill, EaseMode, IsFromMode, () => FromValue_Color, UseCurve, Curve).SetLoop(LoopCount, LoopType).SetLoopingDelay(LoopDelay).SetDelay(Delay).OnStart(() =>
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
            else if (TweenTypes_XHudManager == XTweenTypes_XHudManager.焦散遮罩透明度_BlurMaskOpacity)
            {
                CurrentTweener = XTween.xt_BlurMask_Opacity_To(XTween_Dashboard.HudManagerGet(), EndValue_Float, Duration, IsAutoKill, EaseMode, IsFromMode, () => FromValue_Float, UseCurve, Curve).SetLoop(LoopCount, LoopType).SetLoopingDelay(LoopDelay).SetDelay(Delay).OnStart(() =>
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
            else if (TweenTypes_XHudManager == XTweenTypes_XHudManager.焦散遮罩强度_BlurMaskStrength)
            {
                CurrentTweener = XTween.xt_BlurMask_Strength_To(XTween_Dashboard.HudManagerGet(), EndValue_Float, Duration, IsAutoKill, EaseMode, IsFromMode, () => FromValue_Float, UseCurve, Curve).SetLoop(LoopCount, LoopType).SetLoopingDelay(LoopDelay).SetDelay(Delay).OnStart(() =>
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
            else if (TweenTypes_XHudManager == XTweenTypes_XHudManager.焦散遮罩颜色_BlurMaskColor)
            {
                CurrentTweener = XTween.xt_BlurMask_Color_To(XTween_Dashboard.HudManagerGet(), EndValue_Color, Duration, IsAutoKill, EaseMode, IsFromMode, () => FromValue_Color, UseCurve, Curve).SetLoop(LoopCount, LoopType).SetLoopingDelay(LoopDelay).SetDelay(Delay).OnStart(() =>
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
            else if (TweenTypes_XHudManager == XTweenTypes_XHudManager.全局字体大小缩放_GlobalFontSize)
            {
                CurrentTweener = XTween.xt_GlobalFontSize_To(XTween_Dashboard.HudManagerGet(), EndValue_Float, Duration, IsAutoKill, EaseMode, IsFromMode, () => FromValue_Float, UseCurve, Curve).SetLoop(LoopCount, LoopType).SetLoopingDelay(LoopDelay).SetDelay(Delay).OnStart(() =>
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
            else if (TweenTypes_XHudManager == XTweenTypes_XHudManager.全局音量控制_SoundVolume)
            {
                CurrentTweener = XTween.xt_SoundVolume_To(XTween_Dashboard.HudManagerGet(), EndValue_Float, Duration, IsAutoKill, EaseMode, IsFromMode, () => FromValue_Float, UseCurve, Curve).SetLoop(LoopCount, LoopType).SetLoopingDelay(LoopDelay).SetDelay(Delay).OnStart(() =>
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
            else if (TweenTypes_XHudManager == XTweenTypes_XHudManager.动画持续时间比例缩放_TweenDurationMultiply)
            {
                CurrentTweener = XTween.xt_TweenDurationMultiply_To(XTween_Dashboard.HudManagerGet(), EndValue_Float, Duration, IsAutoKill, EaseMode, IsFromMode, () => FromValue_Float, UseCurve, Curve).SetLoop(LoopCount, LoopType).SetLoopingDelay(LoopDelay).SetDelay(Delay).OnStart(() =>
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
            else if (TweenTypes_XHudManager == XTweenTypes_XHudManager.布局边距_Margins)
            {
                CurrentTweener = XTween.xt_Margins_To(XTween_Dashboard.HudManagerGet(), EndValue_Vector4, Duration, IsAutoKill, EaseMode, IsFromMode, () => FromValue_Vector4, UseCurve, Curve).SetLoop(LoopCount, LoopType).SetLoopingDelay(LoopDelay).SetDelay(Delay).OnStart(() =>
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
            else if (TweenTypes_XHudManager == XTweenTypes_XHudManager.布局水平缩放_MarginHorizontal)
            {
                CurrentTweener = XTween.xt_MarginHorizontal_To(XTween_Dashboard.HudManagerGet(), EndValue_Float, Duration, IsAutoKill, EaseMode, IsFromMode, () => FromValue_Float, UseCurve, Curve).SetLoop(LoopCount, LoopType).SetLoopingDelay(LoopDelay).SetDelay(Delay).OnStart(() =>
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
            else if (TweenTypes_XHudManager == XTweenTypes_XHudManager.布局垂直缩放_MarginVertical)
            {
                CurrentTweener = XTween.xt_MarginVertical_To(XTween_Dashboard.HudManagerGet(), EndValue_Float, Duration, IsAutoKill, EaseMode, IsFromMode, () => FromValue_Float, UseCurve, Curve).SetLoop(LoopCount, LoopType).SetLoopingDelay(LoopDelay).SetDelay(Delay).OnStart(() =>
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
            else if (TweenTypes_XHudManager == XTweenTypes_XHudManager.布局整体缩放_MarginMultiply)
            {
                CurrentTweener = XTween.xt_MarginMultiply_To(XTween_Dashboard.HudManagerGet(), EndValue_Float, Duration, IsAutoKill, EaseMode, IsFromMode, () => FromValue_Float, UseCurve, Curve).SetLoop(LoopCount, LoopType).SetLoopingDelay(LoopDelay).SetDelay(Delay).OnStart(() =>
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
        }
    }
}
