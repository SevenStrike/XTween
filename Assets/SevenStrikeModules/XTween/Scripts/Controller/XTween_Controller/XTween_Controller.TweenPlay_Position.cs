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
