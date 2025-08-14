namespace SevenStrikeModules.XTween
{
    using UnityEngine;

    public partial class XTween_Controller
    {
        /// <summary>
        /// 动画 - 路径方法
        /// </summary>
        private void TweenPlay_Path()
        {
            if (TweenTypes != XTweenTypes.路径_Path)
                return;
            CurrentTweener = XTween.xt_PathMove(Target_RectTransform, Target_PathTool, Duration, Target_PathTool.PathOrientation, Target_PathTool.PathOrientationVector, IsAutoKill, EaseMode, UseCurve, Curve).SetDelay(Delay).SetLoop(LoopCount, LoopType).SetLoopingDelay(LoopDelay).OnStart(() =>
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
            return;
        }
    }
}
