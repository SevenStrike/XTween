namespace SevenStrikeModules.XTween
{
    using UnityEngine;

    public partial class XTween_Controller
    {
        /// <summary>
        /// 动画 - 震动方法
        /// </summary>
        private void TweenPlay_Shake()
        {
            if (TweenTypes != XTweenTypes.震动_Shake)
                return;

            if (TweenTypes_Shakes == XTweenTypes_Shakes.位置_Position)
            {
                CurrentTweener = XTween.xt_ShakePosition(Target_RectTransform, EndValue_Vector3, Vibrato, Randomness, Duration, IsAutoKill, EaseMode, IsFromMode, () => FromValue_Vector3, UseCurve, Curve).SetLoop(LoopCount, LoopType).SetLoopingDelay(LoopDelay).SetDelay(Delay).OnStart(() =>
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
            else if (TweenTypes_Shakes == XTweenTypes_Shakes.旋转_Rotation)
            {
                CurrentTweener = XTween.xt_ShakeRotation(Target_RectTransform, EndValue_Vector3, Vibrato, Randomness, Duration, IsAutoKill, EaseMode, IsFromMode, () => FromValue_Vector3, UseCurve, Curve).SetLoop(LoopCount, LoopType).SetLoopingDelay(LoopDelay).SetDelay(Delay).OnStart(() =>
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
            else if (TweenTypes_Shakes == XTweenTypes_Shakes.缩放_Scale)
            {
                CurrentTweener = XTween.xt_ShakeScale(Target_RectTransform, EndValue_Vector3, Vibrato, Randomness, FadeShake, Duration, IsAutoKill, EaseMode, IsFromMode, () => FromValue_Vector3, UseCurve, Curve).SetLoop(LoopCount, LoopType).SetLoopingDelay(LoopDelay).SetDelay(Delay).OnStart(() =>
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
            else if (TweenTypes_Shakes == XTweenTypes_Shakes.尺寸_Size)
            {
                CurrentTweener = XTween.xt_ShakeSize(Target_RectTransform, EndValue_Vector2, Vibrato, Randomness, FadeShake, Duration, IsAutoKill, EaseMode, IsFromMode, () => FromValue_Vector2, UseCurve, Curve).SetLoop(LoopCount, LoopType).SetLoopingDelay(LoopDelay).SetDelay(Delay).OnStart(() =>
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
}
