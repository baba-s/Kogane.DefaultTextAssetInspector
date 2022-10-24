using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace Kogane.Internal
{
    [InitializeOnLoad]
    internal sealed class DefaultTextAssetInspector : IDefaultAssetInspector
    {
        static DefaultTextAssetInspector()
        {
            DefaultAssetInspector.Add( new DefaultTextAssetInspector() );
        }

        void IDefaultAssetInspector.OnInspectorGUI( Object target )
        {
            var path = AssetDatabase.GetAssetPath( target );

            if ( !path.EndsWith( ".json" ) &&
                 !path.EndsWith( ".xml" ) &&
                 !path.EndsWith( ".properties" ) &&
                 !path.EndsWith( ".plist" ) )
            {
                return;
            }

            var content = File.ReadAllText( path, Encoding.UTF8 );

            var enabled = GUI.enabled;

            try
            {
                GUI.enabled = true;
                EditorGUILayout.TextArea( content );
            }
            finally
            {
                GUI.enabled = enabled;
            }
        }
    }
}