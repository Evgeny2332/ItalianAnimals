using System;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

namespace _Project.Scripts.Services.Save
{
    public sealed class JsonSaveLoadService : ISaveLoadService
    {
        private readonly string _persistentDataPath;

        public JsonSaveLoadService(string persistentDataPath) =>
            _persistentDataPath = persistentDataPath;

        public bool Save<T>(string key, T data)
        {
            if (!IsKeyValid(key))
                return false;

            try
            {
                string json = JsonConvert.SerializeObject(data, Formatting.Indented);
                File.WriteAllText(GetPath(key), json);
                return true;
            }
            catch (Exception exception)
            {
                Debug.LogError($"Не удалось сохранить {key}: {exception.Message}");
                return false;
            }
        }

        public bool TryLoad<T>(string key, out T data)
        {
            data = default;

            if (!IsKeyValid(key))
                return false;

            string path = GetPath(key);

            if (!File.Exists(path))
                return false;

            try
            {
                data = JsonConvert.DeserializeObject<T>(File.ReadAllText(path));
                return data != null;
            }
            catch (Exception exception)
            {
                Debug.LogError($"Не удалось загрузить {key}: {exception.Message}");
                return false;
            }
        }

        public bool Delete(string key)
        {
            if (!IsKeyValid(key) || !File.Exists(GetPath(key)))
                return false;

            try
            {
                File.Delete(GetPath(key));
                return true;
            }
            catch (Exception exception)
            {
                Debug.LogError($"Не удалось удалить {key}: {exception.Message}");
                return false;
            }
        }

        private bool IsKeyValid(string key) =>
            !string.IsNullOrWhiteSpace(key);

        private string GetPath(string key) =>
            Path.Combine(_persistentDataPath, $"{key}.json");
    }
}
