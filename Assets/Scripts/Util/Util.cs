using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nizu.Util
{
    public static class Util
    {
        public static TextMesh CreateTextInWorld(string text, Transform parent = null, Vector3 localpos = default(Vector3), int fontsize = 35, TextAnchor textAnchor = TextAnchor.UpperLeft)
        {
            GameObject gameObject = new("Text_in_World",typeof(TextMesh));
            Transform transform = gameObject.transform;
            transform.SetParent(parent, false);
            transform.transform.localPosition = localpos;
            TextMesh textMesh = gameObject.GetComponent<TextMesh>();
            textMesh.text = text;
            textMesh.fontSize = fontsize;
            textMesh.anchor = textAnchor;
            return textMesh;
        }

        public static Vector3 GetMousePositionInWorld2D()
        {
            Vector3 mousePosition = GetMousePositionInWorld();
            mousePosition.z = 0;
            return mousePosition;
        }

        public static Vector3 GetMousePositionInWorld()
        {
            return GetMousePositionInWorld(Input.mousePosition, Camera.main);
        }

        public static Vector3 GetMousePositionInWorld( Camera worldCamera)
        {
            return GetMousePositionInWorld(Input.mousePosition, worldCamera);
        }

        public static Vector3 GetMousePositionInWorld(Vector3 screenPosition, Camera worldCamera)
        {
            Vector3 worldPosition = worldCamera.ScreenToWorldPoint(screenPosition);
            return worldPosition;
        }


        public static class DebugHelper
        {
            public static void DebugCross(Vector3 pos, Color col = default(Color), float time = 1f, int length = 20)
            {
                Vector3 v1 = new Vector3(length, 0, 0);
                Vector3 v2 = new Vector3(0, length, 0);
                Debug.DrawLine(pos - v1 - v2, pos + v1 + v2, col, time);
                Debug.DrawLine(pos - v1 + v2, pos + v1 - v2, col, time);
            }
        }
        public static class ArrayHelper
        {
            public static T[,] ResizeArray<T>(T[,] original, int x, int y)
            {
                T[,] newArray = new T[x, y];
                int minX = Math.Min(original.GetLength(0), newArray.GetLength(0));
                int minY = Math.Min(original.GetLength(1), newArray.GetLength(1));

                for (int i = 0; i < minY; ++i)
                    Array.Copy(original, i * original.GetLength(0), newArray, i * newArray.GetLength(0), minX);

                return newArray;
            }
        }
    }

}