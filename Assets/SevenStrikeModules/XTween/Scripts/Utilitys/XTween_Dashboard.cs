namespace SevenStrikeModules.XTween
{
    using UnityEditor;
#if UNITY_EDITOR
    using UnityEditor.Callbacks;
#endif
    using UnityEngine;

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
        public static string path_XTween_GUISTYLE = "Assets/SevenStrikeModules/XTween/GUI/Editor/XTweenGuiStyle/";
        public static string path_XTween_GUIROOT = "Assets/SevenStrikeModules/XTween/GUI/Editor/";
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
        /// 获取 XTween GUIROOT路径，根目录：SevenStrikeModules/XTween/GUI/Editor/
        /// </summary>
        /// <returns></returns>
        public static string Get_XTween_GUIRoot_Path()
        {
            return path_XTween_GUIROOT;
        }
        /// <summary>
        /// 获取 XTween GUISTYLE路径，根目录：SevenStrikeModules/XTween/GUI/Editor/XTweenGuiStyle
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

        #region TweenController样式配置
        /// <summary>
        /// TweenController的预览液晶显示器的LED指示器在预览时是否闪烁
        /// </summary>
        public static bool Tween_LiquidLEDBlink = true;
        #endregion

        #region 液晶面板预览样式配置
        /// <summary>
        /// 预览液晶显示器是否使用复古的扫描效果
        /// </summary>
        public static bool LiquidScanStyle = true;
        /// <summary>
        /// 预览液晶显示器的颜色是否使用传统橄榄绿色
        /// </summary>
        public static bool LiquidLagacyColor = true;
        /// <summary>
        /// 预览液晶显示器是否使用肮脏污迹
        /// </summary>
        public static bool LiquidDirty = true;
        /// <summary>
        /// 预览液晶显示器是否使用动画进度条
        /// </summary>
        public static bool LiquidProgressAnimation = true;

        public static Color Liquid_On_Color = new Color(0.6322733f, 0.695f, 0.448275f, 0.8f);
        public static Color Liquid_Off_Color = new Color(0.7893765f, 0.8584906f, 0.6206887f, 0.85f);

#if UNITY_EDITOR
        [InitializeOnEnterPlayMode]
        [DidReloadScripts]
        public static void LoadLiquidStyle()
        {
            if (XTween_Utilitys.PlayerPrefs_KeyIsExist_ForEditor("LiquidLagacyColor"))
                LiquidLagacyColor = XTween_Utilitys.PlayerPrefs_ReadValue_Bool_ForEditor("LiquidLagacyColor");
            else
            {
                LiquidLagacyColor = true;
                XTween_Utilitys.PlayerPrefs_SaveValue_ForEditor("LiquidLagacyColor", LiquidLagacyColor);
            }

            if (XTween_Utilitys.PlayerPrefs_KeyIsExist_ForEditor("LiquidScanStyle"))
                LiquidScanStyle = XTween_Utilitys.PlayerPrefs_ReadValue_Bool_ForEditor("LiquidScanStyle");
            else
            {
                LiquidScanStyle = true;
                XTween_Utilitys.PlayerPrefs_SaveValue_ForEditor("LiquidScanStyle", LiquidScanStyle);
            }

            if (XTween_Utilitys.PlayerPrefs_KeyIsExist_ForEditor("LiquidProgressAnimation"))
                LiquidProgressAnimation = XTween_Utilitys.PlayerPrefs_ReadValue_Bool_ForEditor("LiquidProgressAnimation");
            else
            {
                LiquidProgressAnimation = true;
                XTween_Utilitys.PlayerPrefs_SaveValue_ForEditor("LiquidProgressAnimation", LiquidProgressAnimation);
            }

            if (XTween_Utilitys.PlayerPrefs_KeyIsExist_ForEditor("LiquidDirty"))
                LiquidDirty = XTween_Utilitys.PlayerPrefs_ReadValue_Bool_ForEditor("LiquidDirty");
            else
            {
                LiquidDirty = true;
                XTween_Utilitys.PlayerPrefs_SaveValue_ForEditor("LiquidDirty", LiquidDirty);
            }
        }
#endif
        #endregion
    }
}