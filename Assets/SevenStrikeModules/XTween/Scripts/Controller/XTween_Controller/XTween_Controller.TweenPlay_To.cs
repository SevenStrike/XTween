namespace SevenStrikeModules.XTween
{
    using UnityEngine;

    public partial class XTween_Controller
    {
        /// <summary>
        /// 动画 - 原始方法
        /// </summary>
        private void TweenPlay_To()
        {
            if (TweenTypes != XTweenTypes.原生动画_To)
                return;

            switch (TweenTypes_To)
            {
                case XTweenTypes_To.整数_Int:
                    CurrentTweener = XTween.To(() => Target_Int, x => Target_Int = x, EndValue_Int, Duration, IsAutoKill, EaseMode, IsFromMode, () => FromValue_Int, UseCurve, Curve).SetLoop(LoopCount, LoopType).SetLoopingDelay(LoopDelay).SetDelay(Delay).OnStart(() =>
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
                    break;
                case XTweenTypes_To.浮点数_Float:
                    CurrentTweener = XTween.To(() => Target_Float, x => Target_Float = x, EndValue_Float, Duration, IsAutoKill, EaseMode, IsFromMode, () => FromValue_Float, UseCurve, Curve).SetLoop(LoopCount, LoopType).SetLoopingDelay(LoopDelay).SetDelay(Delay).OnStart(() =>
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
                    break;
                case XTweenTypes_To.字符串_String:
                    CurrentTweener = XTween.To(() => Target_String, x => Target_String = x, IsExtendedString, EndValue_String, Duration, IsAutoKill, EaseMode, IsFromMode, () => FromValue_String, UseCurve, Curve).SetLoop(LoopCount, LoopType).SetLoopingDelay(LoopDelay).SetDelay(Delay).OnStart(() =>
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
                    break;
                case XTweenTypes_To.二维向量_Vector2:
                    CurrentTweener = XTween.To(() => Target_Vector2, x => Target_Vector2 = x, EndValue_Vector2, Duration, IsAutoKill, EaseMode, IsFromMode, () => FromValue_Vector2, UseCurve, Curve).SetLoop(LoopCount, LoopType).SetLoopingDelay(LoopDelay).SetDelay(Delay).OnStart(() =>
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
                    break;
                case XTweenTypes_To.三维向量_Vector3:
                    CurrentTweener = XTween.To(() => Target_Vector3, x => Target_Vector3 = x, EndValue_Vector3, Duration, IsAutoKill, EaseMode, IsFromMode, () => FromValue_Vector3, UseCurve, Curve).SetLoop(LoopCount, LoopType).SetLoopingDelay(LoopDelay).SetDelay(Delay).OnStart(() =>
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
                    break;
                case XTweenTypes_To.四维向量_Vector4:
                    CurrentTweener = XTween.To(() => Target_Vector4, x => Target_Vector4 = x, EndValue_Vector4, Duration, IsAutoKill, EaseMode, IsFromMode, () => FromValue_Vector4, UseCurve, Curve).SetLoop(LoopCount, LoopType).SetLoopingDelay(LoopDelay).SetDelay(Delay).OnStart(() =>
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
                    break;
                case XTweenTypes_To.颜色_Color:
                    CurrentTweener = XTween.To(() => Target_Color, x => Target_Color = x, EndValue_Color, Duration, IsAutoKill, EaseMode, IsFromMode, () => FromValue_Color, UseCurve, Curve).SetLoop(LoopCount, LoopType).SetLoopingDelay(LoopDelay).SetDelay(Delay).OnStart(() =>
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
                    break;
                default:
                    break;
            }
        }
    }
}
