using System.IO;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UIElements;

[CustomEditor(typeof(demo_size_items))]
public class editor_demo_size_items : editor_demo_base
{
    private demo_size_items demo_size;

    public override void OnEnable()
    {
        base.OnEnable();

        demo_size = target as demo_size_items;
    }

    public override VisualElement ExtensionsDraw()
    {
        VisualElement root = base.ExtensionsDraw();

        demoGUI_Line(root);

        // 按钮集合容器
        VisualElement root_btns = new VisualElement
        {
            style = {
                flexDirection = new StyleEnum<FlexDirection>( FlexDirection.Row),
                flexWrap = new StyleEnum<Wrap>(Wrap.Wrap)
            }
        };

        // 按钮 - 打开背包
        Button btn_bag_open = demoGUI_Button("打开背包", Color.green);
        btn_bag_open.clicked += (() =>
        {
            editor_btn_clicked_bag_open();
        });

        // 按钮 - 关闭背包
        Button btn_bag_close = demoGUI_Button("关闭背包", Color.red);
        btn_bag_close.clicked += (() =>
        {
            editor_btn_clicked_bag_close();
        });

        root_btns.Add(btn_bag_open);
        root_btns.Add(btn_bag_close);

        // 按钮 - 读取图片
        Button btn_loadimgs = demoGUI_Button("读取图片", Color.yellow);
        btn_loadimgs.clicked += (() =>
        {
            UnityEngine.SceneManagement.Scene scene = EditorSceneManager.GetActiveScene();
            string path_scene = scene.path;
            string path_root = Path.GetDirectoryName(path_scene) + "/";
            string path_sprites = path_root + "Sprites/Icons/";

            // 搜索指定扩展名的文件（不包含子目录）
            string searchPattern = $"*.png";
            string[] files = Directory.GetFiles(path_sprites, searchPattern, SearchOption.TopDirectoryOnly);

            Debug.Log($"在目录 {path_sprites} 中找到 {files.Length} 个 PNG 文件");

            demo_size.itemstructs.Clear();

            for (int i = 0; i < files.Length; i++)
            {
                string icon_path = files[i];
                Sprite tex = (Sprite)AssetDatabase.LoadAssetAtPath(icon_path, typeof(Sprite));

                itemstruct ist = new itemstruct();
                ist.name = tex.name;
                ist.spr = tex;
                demo_size.itemstructs.Add(ist);
            }
        });

        root.Add(root_btns);
        root.Add(btn_loadimgs);
        //}
        return root;
    }

    #region 按钮动作 - 重写
    /// <summary>
    /// 背包打开
    /// </summary>
    private void editor_btn_clicked_bag_open()
    {
        //if (Application.isPlaying)
        //{
        demo_size.dir = "open";
        demo_size.Tween_Create();
        demo_size.Tween_Play();
        //}
    }
    /// <summary>
    /// 背包关闭
    /// </summary>
    private void editor_btn_clicked_bag_close()
    {
        //if (Application.isPlaying)
        //{
        demo_size.dir = "close";
        demo_size.Tween_Create();
        demo_size.Tween_Play();
        //}
    }

    /// <summary>
    /// 按钮事件 - 创建并播放
    /// </summary>
    public override void editor_btn_clicked_CreateAndPlay()
    {
        // 如果在运行
        if (Application.isPlaying)
        {
            if (demo_size.currentTweener == null)
            {   // 创建动画
                demo_size.Tween_Create();
            }
            else
            {
                // 倒退动画
                demo_size.Tween_Rewind();
            }
            // 播放动画
            demo_size.Tween_Play();
        }
        //如果没运行
        else
        {
            // 创建动画
            demo_size.Tween_Create();
            AppendToPreviewer(demo_size.titleRegionTween);
            // 预览动画
            Preview_Start();
        }
    }
    /// <summary>
    /// 按钮事件 - 倒退
    /// </summary>
    public override void editor_btn_clicked_Rewind()
    {
        base.editor_btn_clicked_Rewind();
    }
    /// <summary>
    /// 按钮事件 - 暂停或继续
    /// </summary>
    public override void editor_btn_clicked_PauseOrContinue()
    {
        base.editor_btn_clicked_PauseOrContinue();
    }
    /// <summary>
    /// 按钮事件 - 杀死
    /// </summary>
    public override void editor_btn_clicked_Kill()
    {
        base.editor_btn_clicked_Kill();

        // 如果在运行
        if (Application.isPlaying)
        {
            demo_size.Tween_Kill();
        }
        //如果没运行
        else
        {
            Preview_Kill();

            if (demo_size.dir == "open")
            {
                demo_size.frame.sizeDelta = demo_size.args_frame.target_open;
                demo_size.titleRegion.sizeDelta = demo_size.args_titleRegion.target_open;
            }
            else
            {
                demo_size.frame.sizeDelta = demo_size.args_frame.target_close;
                demo_size.titleRegion.sizeDelta = demo_size.args_titleRegion.target_close;
            }
        }
    }
    #endregion
}