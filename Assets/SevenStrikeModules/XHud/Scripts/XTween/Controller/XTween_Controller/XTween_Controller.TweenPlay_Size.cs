namespace SevenStrikeModules.XTween
{
    using UnityEngine;
    public partial class XTween_Controller
    {
        /// <summary>
        /// 动画 - 尺寸方法
        /// </summary>
        private void TweenPlay_Size()
        {
            if (TweenTypes != XTweenTypes.尺寸_Size)
                return;

            CurrentTweener = XTween.xt_Size_To(Target_RectTransform, EndValue_Vector2, Duration, IsRelative, IsAutoKill, EaseMode, IsFromMode, () => FromValue_Vector2, UseCurve, Curve).SetLoop(LoopCount, LoopType).SetLoopingDelay(LoopDelay).SetDelay(Delay).OnStart(() =>
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
    }
}
