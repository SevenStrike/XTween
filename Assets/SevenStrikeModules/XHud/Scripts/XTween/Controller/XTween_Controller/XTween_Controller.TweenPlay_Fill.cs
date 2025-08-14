namespace SevenStrikeModules.XTween
{
    public partial class XTween_Controller
    {
        /// <summary>
        /// 动画 - 填充方法
        /// </summary>
        private void TweenPlay_Fill()
        {
            if (TweenTypes != XTweenTypes.填充_Fill)
                return;

            CurrentTweener = XTween.xt_Fill_To(Target_Image, EndValue_Float, Duration, IsAutoKill, EaseMode, IsFromMode, () => FromValue_Float, UseCurve, Curve).SetLoop(LoopCount, LoopType).SetLoopingDelay(LoopDelay).SetDelay(Delay).OnStart(() =>
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
