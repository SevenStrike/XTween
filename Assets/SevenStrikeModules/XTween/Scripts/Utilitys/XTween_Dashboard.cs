/*
 * ============================================================================
 * ⚠️ 版权声明（禁止删除、禁止修改、衍生作品必须保留此注释）⚠️
 * ============================================================================
 * 版权声明 Copyright (C) 2025-Present Nanjing SevenStrike Media Co., Ltd.
 * 中文名称：南京塞维斯传媒有限公司
 * 英文名称：SevenStrikeMedia
 * 项目作者：徐寅智
 * 项目名称：XTween - Unity 高性能动画架构插件
 * 项目启动：2025年8月
 * 官方网站：http://sevenstrike.com/
 * 授权协议：GNU Affero General Public License Version 3 (AGPL 3.0)
 * 协议说明：
 * 1. 你可以自由使用、修改、分发本插件的源代码，但必须保留此版权注释
 * 2. 基于本插件修改后的衍生作品，必须同样遵循 AGPL 3.0 授权协议
 * 3. 若将本插件用于网络服务（如云端Unity编辑器、在线动效生成工具），必须公开修改后的完整源代码
 * 4. 完整协议文本可查阅：https://www.gnu.org/licenses/agpl-3.0.html
 * ============================================================================
 * 违反本注释保留要求，将违反 AGPL 3.0 授权协议，需承担相应法律责任
 */
namespace SevenStrikeModules.XTween
{
    using UnityEditor;
#if UNITY_EDITOR
    using UnityEditor.Callbacks;
#endif
    using UnityEngine;

    public class TweenConfigData
    {
        public bool LiquidScanStyle;
        public bool LiquidDirty;
        public int LiquidBlinker;
        public Color Liquid_On_Color;
        public Color Liquid_Off_Color;
        public int PoolCount_Int;
        public int PoolCount_Float;
        public int PoolCount_String;
        public int PoolCount_Vector2;
        public int PoolCount_Vector3;
        public int PoolCount_Vector4;
        public int PoolCount_Quaternion;
        public int PoolCount_Color;
    }

    public static class XTween_Dashboard
    {
        /// <summary>
        /// 动画全局速率缩放
        /// </summary>
        public static float DurationMultiply = 1;

        #region ThemeColor 主题色
#pragma warning disable CS0414
        private static readonly string PrefsKeyColor_Theme = "XTween-MANAGER-COLOR-THEME";
        private static readonly string PrefsKeyColor_Theme_GP = "XTween-MANAGER-COLOR-THEME-GROUP";
        private static readonly string PrefsKeyColor_Theme_SEP = "XTween-MANAGER-COLOR-THEME-SEPERATE";
#pragma warning restore CS0414
        public static Color Theme_Primary { get; set; } = XTween_Utilitys.ConvertHexStringToColor("#3BFE9B");
        public static Color Theme_Group { get; set; } = XTween_Utilitys.ConvertHexStringToColor("#1E1E1E");
        public static Color Theme_SeperateLine { get; set; } = XTween_Utilitys.ConvertHexStringToColor("#535353");

        public static string Version { get; set; }

#if UNITY_EDITOR
        [DidReloadScripts]
        public static void LoadThemes()
        {
            Color color_theme = XTween_Utilitys.ConvertHexStringToColor("3BFE9B");
            if (!XTween_Utilitys.PlayerPrefs_KeyIsExist_ForEditor(PrefsKeyColor_Theme))
            {
                XTween_Utilitys.PlayerPrefs_SaveValue_ForEditor(PrefsKeyColor_Theme, $"{color_theme.r},{color_theme.g},{color_theme.b}");
                Theme_Primary = new Color(color_theme.r, color_theme.g, color_theme.b);
            }
            else
            {
                string colorval = XTween_Utilitys.PlayerPrefs_ReadValue_String_ForEditor(PrefsKeyColor_Theme);
                Color v3 = XTween_Utilitys.ConvertStringToColor(colorval + ",1", false);
                Theme_Primary = v3;
            }

            Color color_theme_gp = XTween_Utilitys.ConvertHexStringToColor("1E1E1E");
            if (!XTween_Utilitys.PlayerPrefs_KeyIsExist_ForEditor(PrefsKeyColor_Theme_GP))
            {
                XTween_Utilitys.PlayerPrefs_SaveValue_ForEditor(PrefsKeyColor_Theme_GP, $"{color_theme_gp.r},{color_theme_gp.g},{color_theme_gp.b}");
                Theme_Group = new Color(color_theme_gp.r, color_theme_gp.g, color_theme_gp.b);
            }
            else
            {
                string colorval = XTween_Utilitys.PlayerPrefs_ReadValue_String_ForEditor(PrefsKeyColor_Theme_GP);
                Color v3 = XTween_Utilitys.ConvertStringToColor(colorval + ",1", false);
                Theme_Group = v3;
            }

            Color color_theme_sep = XTween_Utilitys.ConvertHexStringToColor("535353");
            if (!XTween_Utilitys.PlayerPrefs_KeyIsExist_ForEditor(PrefsKeyColor_Theme_SEP))
            {
                XTween_Utilitys.PlayerPrefs_SaveValue_ForEditor(PrefsKeyColor_Theme_SEP, $"{color_theme_sep.r},{color_theme_sep.g},{color_theme_sep.b}");
                Theme_SeperateLine = new Color(color_theme_sep.r, color_theme_sep.g, color_theme_sep.b);
            }
            else
            {
                string colorval = XTween_Utilitys.PlayerPrefs_ReadValue_String_ForEditor(PrefsKeyColor_Theme_SEP);
                Color v3 = XTween_Utilitys.ConvertStringToColor(colorval + ",1", false);
                Theme_SeperateLine = v3;
            }
        }
#endif
        #endregion

        #region 公共路径
        public static string path_XTween_GUISTYLE = "Assets/SevenStrikeModules/XTween/GUI/XTweenGuiStyle/";
        public static string path_XTween_GUIROOT = "Assets/SevenStrikeModules/XTween/GUI/";
        public static string path_XTween_ROOT = "Assets/SevenStrikeModules/XTween/";
        public static string path_XTween_MATERIAL = "Assets/SevenStrikeModules/XTween/Materials/";
        public static string path_XTween_PREFABS = "Assets/SevenStrikeModules/XTween/Prefabs/";
        public static string path_XTween_SHADERS = "Assets/SevenStrikeModules/XTween/Shaders/";
        public static string path_XTween_SOUND = "Assets/SevenStrikeModules/XTween/Sound/";
        public static string path_XTween_SPRITES = "Assets/SevenStrikeModules/XTween/Sprites/";
        public static string path_XTween_TEXTURES = "Assets/SevenStrikeModules/XTween/Textures/";
        public static string path_XTween_FONTS = "Assets/SevenStrikeModules/XTween/Fonts/";
        public static string path_XTween_SCRIPTS = "Assets/SevenStrikeModules/XTween/Scripts/";

        #region 路径获取
        /// <summary>
        /// 获取 XTween ROOT路径，根目录：SevenStrikeModules/XTween/
        /// </summary>
        /// <returns></returns>
        public static string Get_XTween_Root_Path()
        {
            return path_XTween_ROOT;
        }
        /// <summary>
        /// 获取 XTween GUIROOT路径，根目录：SevenStrikeModules/XTween/GUI/
        /// </summary>
        /// <returns></returns>
        public static string Get_XTween_GUIRoot_Path()
        {
            return path_XTween_GUIROOT;
        }
        /// <summary>
        /// 获取 XTween GUISTYLE路径，根目录：SevenStrikeModules/XTween/GUI/XTweenGuiStyle
        /// </summary>
        /// <returns></returns>
        public static string Get_path_XTween_GUIStyle_Path()
        {
            return path_XTween_GUISTYLE;
        }
        /// <summary>
        /// 获取 XTween 材质路径，根目录：SevenStrikeModules/XTween/Materials/
        /// </summary>
        /// <returns></returns>
        public static string Get_path_XTween_Materials_Path()
        {
            return path_XTween_MATERIAL;
        }
        /// <summary>
        /// 获取 XTween 预制体路径，根目录：SevenStrikeModules/XTween/Prefabs/
        /// </summary>
        /// <returns></returns>
        public static string Get_path_XTween_PREFABS_Path()
        {
            return path_XTween_PREFABS;
        }
        /// <summary>
        /// 获取 XTween 着色器路径，根目录：SevenStrikeModules/XTween/Shaders/
        /// </summary>
        /// <returns></returns>
        public static string Get_path_XTween_SHADERS_Path()
        {
            return path_XTween_SHADERS;
        }
        /// <summary>
        /// 获取 XTween 声音路径，根目录：SevenStrikeModules/XTween/Sound/
        /// </summary>
        /// <returns></returns>
        public static string Get_path_XTween_SOUND_Path()
        {
            return path_XTween_SOUND;
        }
        /// <summary>
        /// 获取 XTween 精灵路径，根目录：SevenStrikeModules/XTween/Sprites/
        /// </summary>
        /// <returns></returns>
        public static string Get_path_XTween_SPRITES_Path()
        {
            return path_XTween_SPRITES;
        }
        /// <summary>
        /// 获取 XTween 贴图路径，根目录：SevenStrikeModules/XTween/Textures/
        /// </summary>
        /// <returns></returns>
        public static string Get_path_XTween_TEXTURES_Path()
        {
            return path_XTween_TEXTURES;
        }
        /// <summary>
        /// 获取 XTween 字体路径，根目录：SevenStrikeModules/XTween/Fonts/
        /// </summary>
        /// <returns></returns>
        public static string Get_path_XTween_FONTS_Path()
        {
            return path_XTween_FONTS;
        }
        /// <summary>
        /// 获取 XTween 脚本路径，根目录：SevenStrikeModules/XTween/Scripts/
        /// </summary>
        /// <returns></returns>
        public static string Get_path_XTween_SCRIPTS_Path()
        {
            return path_XTween_SCRIPTS;
        }
        #endregion
        #endregion

        #region 液晶面板预览样式配置
        [SerializeField]
        public static TweenConfigData TweenConfigData;

#if UNITY_EDITOR
        [InitializeOnEnterPlayMode]
        [DidReloadScripts]
        public static void LoadLiquidStyle()
        {
            //获取配置文件
            string json = AssetDatabase.LoadAssetAtPath<TextAsset>(Get_XTween_GUIRoot_Path() + $"XTweenVisualStyle.json").text;
            TweenConfigData = JsonUtility.FromJson<TweenConfigData>(json);
        }
#endif
        #endregion
    }
}