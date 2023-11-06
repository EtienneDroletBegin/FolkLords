using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class Delegate : MonoBehaviour
{
    public delegate void NoReturn();
    public delegate string Return();
    public delegate void WithParam(string _text);
    public delegate bool Condition(string _text);


    private Condition condition;
    private WithParam withParam;
    private NoReturn noReturn;
    private Return Dreturn;


    private Predicate<string> predicate;


    private Action action;
    private Action<string> someActionWithString;


    private Player player;
    public Button button;
    public List<string> listStrings = new List<string>();
    private void Start()
    {
        player = new Player();
        ExampleDelegate();

    }
     private void Test()
    {
        Debug.Log("Func no Return");
    }
    public void ExampleDelegate()
    {
        NoReturn n = delegate
        {
            Debug.Log("Executing noReturn delegate! ");
        };
        noReturn += n;

        noReturn += () =>
        {
            Debug.Log("LambDad No Refunds");
        };
        noReturn += Test;
        noReturn?.Invoke();


        Return r1 = delegate
        {
            return "Return Delegate 1";
        };
        Return r2 = delegate
        {
            return "Return Delegate 2";
        };

        Dreturn += r1;
        Dreturn += r2;
        Dreturn += () =>
        {
            return "Return Delegate 3";
        };
        Debug.Log(Dreturn?.Invoke());

        Condition c = delegate (string _text)
        {
            foreach (char item in _text)
            {
                if (char.IsDigit(item))
                {
                    return true;
                }
                
            }
            return false;
        };
        condition += c;
        Debug.Log(condition?.Invoke("YAAAAhhh4"));

        
    }

}
    public class Player
    {
        public event Action onPlayerDeath;
        public void Dead()
        {
            onPlayerDeath?.Invoke();
        }
    }
