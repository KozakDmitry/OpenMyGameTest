using Assets.Project.CodeBase.Infostructure.Services.SceneService;
using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Diagnostics;

public class DIContainer
{
    static DIContainer instance;
    readonly Dictionary<Type, DIRegistration> _registrations = new();
    readonly Dictionary<Type, DISubscribe> _subscribe = new();



    public static DIContainer Get()
    {
        if (instance == null) instance = new();

        return instance;
    }
    public DIContainer()
    {
    }

    /// <summary>
    /// зарегистрировать обьект
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="instance"></param>
    /// <param name="mode">на всю игру или до перезагрузки сцены</param>
    public void Register<T>(T instance)
    {
        if (_registrations.ContainsKey(typeof(T)))
        {
            Debug.Log("DI: Singltone is contained! " + typeof(T).FullName);
            return;
        }

        _registrations[typeof(T)] = new DIRegistration()
        {
            Instance = instance,
        };

        if (_subscribe.TryGetValue(typeof(T), out var subscribe))
        {
            subscribe?.CallbackSubscribe?.Invoke(instance);
            _subscribe.Remove(typeof(T));
        }

    }

    /// <summary>
    /// подписаться на получение обьекта или получить обьект через колбэк сразу
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="Callback"></param>
    public void Resolve<T>(Action<object> Callback)
    {
        if (_registrations.TryGetValue(typeof(T), out var Registration)){
            if (Registration.Instance != null) Callback?.Invoke((T)Registration.Instance);
        }
        else
        {
            if (!_subscribe.ContainsKey(typeof(T)))
            {
                _subscribe[typeof(T)] = new DISubscribe()
                {
                    Type = typeof(T),
                    CallbackSubscribe = Callback
                };
            }
            else _subscribe[typeof(T)].CallbackSubscribe += Callback;
        }
    }

    /// <summary>
    /// Сразу возвращает обьект,  ни куда не подписывается. если такого нет, возвращает null или дефолтное значение для типа
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public T ResolveSync<T>()
    {
        if (_registrations.TryGetValue(typeof(T), out var Registration))
        {
            if (Registration.Instance != null) return (T)Registration.Instance;
        }

        return default;
    }

 

    public void Remove<T>(T instance)
    {
        _registrations.Remove(typeof(T));
    }
}
