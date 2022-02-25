using UnityEngine;

namespace Assets.Scripts
{
    class RoomSerializer
    {
        /// <summary>
        /// NOTE: THIS STORES YOUR ROOM IN C:\Users\<user>\AppData\LocalLow\DefaultCompany\brackeys-feb-22-jam
        /// </summary>
        /// <param name="id"></param>
        /// <param name="room"></param>
        public void SaveRoom(int id, RoomConfig room)
        {
            var outStream = System.IO.File.CreateText($"{Application.persistentDataPath}/{id}.txt");
            outStream.WriteLine(JsonUtility.ToJson(room));
            outStream.Close();
        }

        public RoomConfig GetRoom(int id)
        {
            var roomText = Resources.Load<TextAsset>($"Rooms/{id}");
            return JsonUtility.FromJson<RoomConfig>(roomText.text);
        }
    }
}
