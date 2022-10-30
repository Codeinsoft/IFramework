using IFramework.Infrastructure.Transversal.Cache.Interfaces;
using IFramework.Infrastructure.Transversal.Cache.Redis.Config;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;

namespace IFramework.Infrastructure.Transversal.Cache.Redis.Service
{
    public class RedisCacheRepository<TValue> : ICacheRepository<TValue>
       where TValue : new()
    {
        internal readonly IDatabase Db;
        protected readonly IRedisConnectionFactory ConnectionFactory;

        #region hashmap
        //protected PropertyInfo[] Properties => this.Type.GetProperties();
        //protected Type Type => typeof(TValue); 
        #endregion

        public RedisCacheRepository(IRedisConnectionFactory connectionFactory)
        {
            this.ConnectionFactory = connectionFactory;
            this.Db = this.ConnectionFactory.Connection().GetDatabase();
        }

        public void Delete(string key)
        {
            this.Db.KeyDelete(key);
        }

        public TValue Get(string key)
        {

            if (this.KeyExists(key))
            {
                var cacheValue = this.Db.StringGet(key);
                return JsonConvert.DeserializeObject<TValue>(cacheValue, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
            }

            return default(TValue);

        }

        /// <summary>
        //     Sets the specified fields to their respective values in the hash stored at key.
        //     This command overwrites any specified fields that already exist in the hash,
        //     leaving other unspecified fields untouched. If key does not exist, a new key
        //     holding a hash is created.
        /// </summary>
        /// <param name="request"></param>
        public void Set(string key, object value, int duration)
        {
            this.Db.KeyExpire(key, DateTime.Now.AddMilliseconds(duration));
            this.Db.StringSet(key, JsonConvert.SerializeObject(value, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }));
        }

        #region hashmap

        //protected HashEntry[] GenerateHash(TValue obj)
        //{
        //    var props = this.Properties;
        //    var hash = new HashEntry[props.Count()];

        //    // TODO-Arda: iç içe classlılar çalışmıyorsa JSON serialize et
        //    for (var i = 0; i < props.Count(); i++)
        //        hash[i] = new HashEntry(props[i].Name, props[i].GetValue(obj).ToString());

        //    return hash;

        //}

        //protected TValue MapFromHash(HashEntry[] hash)
        //{
        //    var obj = (TValue)Activator.CreateInstance(this.Type); // new instance of T
        //    var props = this.Properties;

        //    for (var i = 0; i < props.Count(); i++)
        //    {
        //        for (var j = 0; j < hash.Count(); j++)
        //        {
        //            if (props[i].Name == hash[j].Name)
        //            {
        //                var val = hash[j].Value;
        //                var type = props[i].PropertyType;

        //                if (type.IsConstructedGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
        //                    if (string.IsNullOrEmpty(val))
        //                    {
        //                        props[i].SetValue(obj, null);
        //                    }
        //                props[i].SetValue(obj, Convert.ChangeType(val, type));
        //            }
        //        }
        //    }
        //    return obj;
        //}

        #endregion

        public bool KeyExists(string key)
        {
            return !string.IsNullOrEmpty(this.Db.StringGet(key));
        }

        public void DeleteKeysByPattern(string pattern)
        {
            if (string.IsNullOrWhiteSpace(pattern))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(pattern));

            foreach (var endpoint in this.ConnectionFactory.Connection().GetEndPoints())
            {
                var server = this.ConnectionFactory.Connection().GetServer(endpoint);

                //TODO: keyasync
                foreach (var key in server.Keys(pattern: pattern))
                {
                    this.Delete(key);
                }
            }
        }

    }
}
