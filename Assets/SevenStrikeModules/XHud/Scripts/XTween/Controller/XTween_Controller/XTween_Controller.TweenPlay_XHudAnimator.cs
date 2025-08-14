namespace SevenStrikeModules.XTween
{
    using UnityEngine;

    public partial class XTween_Controller
    {

        /// <summary>
        /// 动画 - XHud动画器方法
        /// </summary>
        private void TweenPlay_XHudAnimator()
        {
            if (TweenTypes != XTweenTypes.动画器_XHudAnimator)
                return;

            if (TweenTypes_XHudAnimator == XTweenTypes_XHudAnimator.原始色_OriginalColor)
            {
                CurrentTweener = XTween.xt_Original_Color_To(Target_HudAnimator, EndValue_Color, Duration, IsAutoKill, EaseMode, IsFromMode, () => FromValue_Color, UseCurve, Curve).SetLoop(LoopCount, LoopType).SetLoopingDelay(LoopDelay).SetDelay(Delay).OnStart(() =>
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
        }
    }
}
