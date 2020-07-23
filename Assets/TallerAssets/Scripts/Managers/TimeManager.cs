using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; 
using System.Threading.Tasks;

namespace Taller
{
    public class TimeManager : MonoBehaviour
    {

        public static async Task Repeat(IEnumerable action, int repeatCount, float repeatDelay, Delegate OnRepeatFinish = null)
        {

            for (int i = 0; i < repeatCount; i++)
            {
               
                await Task.Delay(Mathf.RoundToInt(repeatDelay * 1000));

            }

            OnRepeatFinish?.DynamicInvoke();

        }
        static async Task RepeatActionInternal(Action action,int repeatCount,float repeatDelay,Delegate OnRepeatFinish=null)
        {
            for (int i = 0; i < repeatCount; i++)
            {
                action.DynamicInvoke();
                 await Task.Delay(Mathf.RoundToInt(repeatDelay * 1000) );

            }

            OnRepeatFinish?.DynamicInvoke();

        }

        public static void RepeatFunction(Action action, int repeatCount, float repeatDelay, Delegate OnRepeatFinish = null)
        {
            
            _ = RepeatActionInternal(action, repeatCount, repeatDelay, OnRepeatFinish);


        } 


       
    }

}
