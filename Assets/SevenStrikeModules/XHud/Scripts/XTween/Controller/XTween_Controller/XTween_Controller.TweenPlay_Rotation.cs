namespace SevenStrikeModules.XTween
{
    using UnityEngine;
    public partial class XTween_Controller
    {
        /// <summary>
        /// 动画 - 旋转方法
        /// </summary>
        private void TweenPlay_Rotation()
        {
            if (TweenTypes != XTweenTypes.旋转_Rotation)
                return;
            if (TweenTypes_Rotations == XTweenTypes_Rotations.欧拉角度_Euler)
            {
                CurrentTweener = XTween.xt_Rotate_To(Target_RectTransform, EndValue_Vector3, Duration, IsRelative, IsAutoKill, EaseMode, IsFromMode, () => FromValue_Vector3, UseCurve, Curve).SetLoop(LoopCount, LoopType).SetLoopingDelay(LoopDelay).SetDelay(Delay).OnStart(() =>
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
            else if (TweenTypes_Rotations == XTweenTypes_Rotations.四元数_Quaternion)
            {
                CurrentTweener = XTween.xt_Rotate_To(Target_RectTransform, EndValue_Quaternion, Duration, IsRelative, IsAutoKill, HudRotateMode, EaseMode, IsFromMode, () => FromValue_Quaternion, UseCurve, Curve).SetLoop(LoopCount, LoopType).SetLoopingDelay(LoopDelay).SetDelay(Delay).OnStart(() =>
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
                }).OnUpdate<Quaternion>((value, linearProgress, time) =>
                {
                    if (act_onUpdate_quaternion != null)
                        act_onUpdate_quaternion(value, linearProgress, time);
                }).OnStepUpdate<Quaternion>((value, linearProgress, elapsedTime) =>
                {
                    if (act_onStepUpdate_quaternion != null)
                        act_onStepUpdate_quaternion(value, linearProgress, elapsedTime);
                }).OnProgress<Quaternion>((value, linearProgress) =>
                {
                    if (act_onProgress_quaternion != null)
                        act_onProgress_quaternion(value, linearProgress);
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
