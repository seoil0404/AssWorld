#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;

namespace Wata.Extension.Test {
    public abstract class TestEditorBase: Editor {
        
        protected bool isFoldOut = true;
        protected Dictionary<MethodInfo, List<(ParameterInfo Info, string Value)>> parameters = new();
        protected static Dictionary<Type, MethodInfo> Parse = new();
        
        protected void SetPropertyField(MethodInfo method) {
            
            parameters.TryAdd(method, new());
            int idx = 0;
                
            foreach (var parameter in method.GetParameters()) {
                var input = "";
                    

                if (idx < parameters[method].Count) {

                    var content = parameters[method][idx].Value;
                    input = EditorGUILayout.TextField($"{parameter.Name}({parameter.ParameterType.Name})", content);
                    parameters[method][idx] = (parameter, input);
                }
                else {
                    input = EditorGUILayout.TextField($"{parameter.Name}({parameter.ParameterType.Name})", "");
                    parameters[method].Add((parameter, input));
                }

                idx++;
            }
        }

        protected object[] GetParameterValue(MethodInfo method) {
            var parameterList = new List<Object>();
                    
            foreach (var parameter in parameters[method]) {

                var parameterType = parameter.Info.ParameterType;
                if (!Parse.ContainsKey(parameterType)) {

                    Parse.Add(parameterType, parameterType.GetMethod("Parse", new[] { typeof(string) }));
                }

                var parsedValue = Parse[parameterType].Invoke(null, new[] { parameter.Value });
                parameterList.Add(parsedValue);
            }

            return parameterList.ToArray();
        }
    }
}
#endif