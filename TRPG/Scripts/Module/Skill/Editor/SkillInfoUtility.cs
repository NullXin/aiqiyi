using UnityEngine;
using UnityEditor;
using TRPG.Module;
namespace TRPG.Module
{
    public class SkillInfoUtility
    {
        /// <summary>
        /// 生成SkillInfo asset文件
        /// </summary>
        [MenuItem( "Tools/Skill/Create Skill Info" )]
        static void Create()
        {
            // 在内存中生成一个SkillInfo的实例
            SkillInfo newSkillInfo = ScriptableObject.CreateInstance<SkillInfo>();
            // 将实例写到本地文件
            AssetDatabase.CreateAsset( newSkillInfo, "Assets/newSkillInfo.asset" );
            // 自动选中生成文件
            Selection.activeObject = newSkillInfo;
        }
    }
}
