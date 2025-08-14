namespace SevenStrikeModules.XTween
{
    using UnityEngine;

    public partial class XTween_Controller
    {
        /// <summary>
        /// 动画 - XHudText方法
        /// </summary>
        private void TweenPlay_XHudText()
        {
            if (TweenTypes != XTweenTypes.文字_XHudText)
                return;

            if (TweenTypes_XHudText == XTweenTypes_XHudText.文字尺寸_FontSize)
            {
                CurrentTweener = XTween.xt_FontSize_To(Target_HudText, EndValue_Int, Duration, IsAutoKill, EaseMode, IsFromMode, () => FromValue_Int, UseCurve, Curve).SetLoop(LoopCount, LoopType).SetLoopingDelay(LoopDelay).SetDelay(Delay).OnStart(() =>
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
                }).OnUpdate<int>((value, linearProgress, time) =>
                {
                    if (act_onUpdate_int != null)
                        act_onUpdate_int(value, linearProgress, time);
                }).OnStepUpdate<int>((value, linearProgress, elapsedTime) =>
                {
                    if (act_onStepUpdate_int != null)
                        act_onStepUpdate_int(value, linearProgress, elapsedTime);
                }).OnProgress<int>((value, linearProgress) =>
                {
                    if (act_onProgress_int != null)
                        act_onProgress_int(value, linearProgress);
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
            else if (TweenTypes_XHudText == XTweenTypes_XHudText.文字行高_LineHeight)
            {
                CurrentTweener = XTween.xt_FontLineHeight_To(Target_HudText, EndValue_Float, Duration, IsAutoKill, EaseMode, IsFromMode, () => FromValue_Float, UseCurve, Curve).SetLoop(LoopCount, LoopType).SetLoopingDelay(LoopDelay).SetDelay(Delay).OnStart(() =>
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
            else if (TweenTypes_XHudText == XTweenTypes_XHudText.文字颜色_Color)
            {
                CurrentTweener = XTween.xt_FontColor_To(Target_HudText, EndValue_Color, Duration, IsAutoKill, EaseMode, IsFromMode, () => FromValue_Color, UseCurve, Curve).SetLoop(LoopCount, LoopType).SetLoopingDelay(LoopDelay).SetDelay(Delay).OnStart(() =>
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
            else if (TweenTypes_XHudText == XTweenTypes_XHudText.文字内容_Content)
            {
                CurrentTweener = XTween.xt_FontText_To(Target_HudText, IsExtendedString, EndValue_String, Duration, IsAutoKill, EaseMode, IsFromMode, () => FromValue_String, UseCurve, Curve).SetLoop(LoopCount, LoopType).SetLoopingDelay(LoopDelay).SetDelay(Delay).OnStart(() =>
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
        }
    }
}
