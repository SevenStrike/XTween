namespace SevenStrikeModules.XTween
{
    using UnityEditor;
#if UNITY_EDITOR
    using UnityEditor.Callbacks;
#endif
    using UnityEngine;

    public static class XTween_Dashboard
    {
        #region ThemeColor 主题色
#pragma warning disable CS0414
        private static readonly string PrefsKeyColor_Theme = "XHUD-MANAGER-COLOR-THEME";
        private static readonly string PrefsKeyColor_Theme_GP = "XHUD-MANAGER-COLOR-THEME-GROUP";
        private static readonly string PrefsKeyColor_Theme_SEP = "XHUD-MANAGER-COLOR-THEME-SEPERATE";
#pragma warning restore CS0414
        public static Color Theme_Primary { get; set; } = util_Tools.Color_From_HexString("#3BFE9B");
        public static Color Theme_Group { get; set; } = util_Tools.Color_From_HexString("#1E1E1E");
        public static Color Theme_SeperateLine { get; set; } = util_Tools.Color_From_HexString("#535353");

        public static string Version { get; set; }

#if UNITY_EDITOR
        [DidReloadScripts]
        public static void LoadThemes()
        {
            Color color_theme = util_Tools.Color_From_HexString("3BFE9B");
            if (!util_Tools.PlayerPrefs_KeyIsExist_ForEditor(PrefsKeyColor_Theme))
            {
                util_Tools.PlayerPrefs_SaveValue_ForEditor(PrefsKeyColor_Theme, $"{color_theme.r},{color_theme.g},{color_theme.b}");
                Theme_Primary = new Color(color_theme.r, color_theme.g, color_theme.b);
            }
            else
            {
                string colorval = util_Tools.PlayerPrefs_ReadValue_String_ForEditor(PrefsKeyColor_Theme);
                Color v3 = util_Tools.Color_From_String(colorval + ",1", false);
                Theme_Primary = v3;
            }

            Color color_theme_gp = util_Tools.Color_From_HexString("1E1E1E");
            if (!util_Tools.PlayerPrefs_KeyIsExist_ForEditor(PrefsKeyColor_Theme_GP))
            {
                util_Tools.PlayerPrefs_SaveValue_ForEditor(PrefsKeyColor_Theme_GP, $"{color_theme_gp.r},{color_theme_gp.g},{color_theme_gp.b}");
                Theme_Group = new Color(color_theme_gp.r, color_theme_gp.g, color_theme_gp.b);
            }
            else
            {
                string colorval = util_Tools.PlayerPrefs_ReadValue_String_ForEditor(PrefsKeyColor_Theme_GP);
                Color v3 = util_Tools.Color_From_String(colorval + ",1", false);
                Theme_Group = v3;
            }

            Color color_theme_sep = util_Tools.Color_From_HexString("535353");
            if (!util_Tools.PlayerPrefs_KeyIsExist_ForEditor(PrefsKeyColor_Theme_SEP))
            {
                util_Tools.PlayerPrefs_SaveValue_ForEditor(PrefsKeyColor_Theme_SEP, $"{color_theme_sep.r},{color_theme_sep.g},{color_theme_sep.b}");
                Theme_SeperateLine = new Color(color_theme_sep.r, color_theme_sep.g, color_theme_sep.b);
            }
            else
            {
                string colorval = util_Tools.PlayerPrefs_ReadValue_String_ForEditor(PrefsKeyColor_Theme_SEP);
                Color v3 = util_Tools.Color_From_String(colorval + ",1", false);
                Theme_SeperateLine = v3;
            }

        }
#endif
        #endregion

        #region 公共路径
        public static string path_GUISTYLE = "Assets/SevenStrikeModules/XHud/GUI/Editor/HudGuiStyle/";
        public static string path_GUIROOT = "Assets/SevenStrikeModules/XHud/GUI/Editor/";
        public static string path_XHUD_ROOT = "Assets/SevenStrikeModules/XHud/";
        public static string path_XHUD_MATERIAL = "Assets/SevenStrikeModules/XHud/Materials/";
        public static string path_XHUD_PREFABS = "Assets/SevenStrikeModules/XHud/Prefabs/";
        public static string path_XHUD_SHADERS = "Assets/SevenStrikeModules/XHud/Shaders/";
        public static string path_XHUD_SOUND = "Assets/SevenStrikeModules/XHud/Sound/";
        public static string path_XHUD_SPRITES = "Assets/SevenStrikeModules/XHud/Sprites/";
        public static string path_XHUD_TEXTURES = "Assets/SevenStrikeModules/XHud/Textures/";
        public static string path_XHUD_FONTS = "Assets/SevenStrikeModules/XHud/Fonts/";
        public static string path_XHUD_SCRIPTS = "Assets/SevenStrikeModules/XHud/Scripts/";

        #region 路径获取
        /// <summary>
        /// 获取XHUD XHUDROOT路径，根目录：SevenStrikeModules/XHud/
        /// </summary>
        /// <returns></returns>
        public static string Get_XHudRoot_Path()
        {
            return path_XHUD_ROOT;
        }
        /// <summary>
        /// 获取XHUD GUIROOT路径，根目录：SevenStrikeModules/XHud/GUI/Editor/
        /// </summary>
        /// <returns></returns>
        public static string Get_GUIRoot_Path()
        {
            return path_GUIROOT;
        }
        /// <summary>
        /// 获取XHUD GUISTYLE路径，根目录：SevenStrikeModules/XHud/GUI/Editor/HudGuiStyle
        /// </summary>
        /// <returns></returns>
        public static string Get_GUIStyle_Path()
        {
            return path_GUISTYLE;
        }
        /// <summary>
        /// 获取XHUD 材质路径，根目录：SevenStrikeModules/XHud/Materials/
        /// </summary>
        /// <returns></returns>
        public static string Get_Materials_Path()
        {
            return path_XHUD_MATERIAL;
        }
        /// <summary>
        /// 获取XHUD 预制体路径，根目录：SevenStrikeModules/XHud/Prefabs/
        /// </summary>
        /// <returns></returns>
        public static string Get_path_XHUD_PREFABS_Path()
        {
            return path_XHUD_PREFABS;
        }
        /// <summary>
        /// 获取XHUD 着色器路径，根目录：SevenStrikeModules/XHud/Shaders/
        /// </summary>
        /// <returns></returns>
        public static string Get_path_XHUD_SHADERS_Path()
        {
            return path_XHUD_SHADERS;
        }
        /// <summary>
        /// 获取XHUD 声音路径，根目录：SevenStrikeModules/XHud/Sound/
        /// </summary>
        /// <returns></returns>
        public static string Get_path_XHUD_SOUND_Path()
        {
            return path_XHUD_SOUND;
        }
        /// <summary>
        /// 获取XHUD 精灵路径，根目录：SevenStrikeModules/XHud/Sprites/
        /// </summary>
        /// <returns></returns>
        public static string Get_path_XHUD_SPRITES_Path()
        {
            return path_XHUD_SPRITES;
        }
        /// <summary>
        /// 获取XHUD 贴图路径，根目录：SevenStrikeModules/XHud/Textures/
        /// </summary>
        /// <returns></returns>
        public static string Get_path_XHUD_TEXTURES_Path()
        {
            return path_XHUD_TEXTURES;
        }
        /// <summary>
        /// 获取XHUD 字体路径，根目录：SevenStrikeModules/XHud/Fonts/
        /// </summary>
        /// <returns></returns>
        public static string Get_path_XHUD_FONTS_Path()
        {
            return path_XHUD_FONTS;
        }
        /// <summary>
        /// 获取XHUD 字体路径，根目录：SevenStrikeModules/XHud/Scripts/
        /// </summary>
        /// <returns></returns>
        public static string Get_path_XHUD_SCRIPTS_Path()
        {
            return path_XHUD_SCRIPTS;
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
            if (util_Tools.PlayerPrefs_KeyIsExist_ForEditor("LiquidLagacyColor"))
                LiquidLagacyColor = util_Tools.PlayerPrefs_ReadValue_Bool_ForEditor("LiquidLagacyColor");
            else
            {
                LiquidLagacyColor = true;
                util_Tools.PlayerPrefs_SaveValue_ForEditor("LiquidLagacyColor", LiquidLagacyColor);
            }

            if (util_Tools.PlayerPrefs_KeyIsExist_ForEditor("LiquidScanStyle"))
                LiquidScanStyle = util_Tools.PlayerPrefs_ReadValue_Bool_ForEditor("LiquidScanStyle");
            else
            {
                LiquidScanStyle = true;
                util_Tools.PlayerPrefs_SaveValue_ForEditor("LiquidScanStyle", LiquidScanStyle);
            }

            if (util_Tools.PlayerPrefs_KeyIsExist_ForEditor("LiquidProgressAnimation"))
                LiquidProgressAnimation = util_Tools.PlayerPrefs_ReadValue_Bool_ForEditor("LiquidProgressAnimation");
            else
            {
                LiquidProgressAnimation = true;
                util_Tools.PlayerPrefs_SaveValue_ForEditor("LiquidProgressAnimation", LiquidProgressAnimation);
            }

            if (util_Tools.PlayerPrefs_KeyIsExist_ForEditor("LiquidDirty"))
                LiquidDirty = util_Tools.PlayerPrefs_ReadValue_Bool_ForEditor("LiquidDirty");
            else
            {
                LiquidDirty = true;
                util_Tools.PlayerPrefs_SaveValue_ForEditor("LiquidDirty", LiquidDirty);
            }
        }
#endif
        #endregion
    }
}