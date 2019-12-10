using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroManager : MonoBehaviour
{

    public MatchManager gm;
    public Player p1;
    public Player p2;
    public InputTranslator it;
    public FightManager fm;
    public BeatManager bm;
    public AkEvent ae;

    public float counter;

    // Start is called before the first frame update
    void Start()
    {
        counter = 4.0f;
    }

    // Update is called once per frame
    void Update()
    {
        counter -= Time.deltaTime;
        if(counter <= 0)
        {
            
            it.enabled = true;
            gm.enabled = true;
            fm.enabled = true;
            bm.enabled = true;
            p1.enabled = true;
            p2.enabled = true;
            ae.useCallbacks = true;
            it.customAwake();
            fm.customAwake();
            bm.customAwake();
            it.customStart();
            fm.customStart();
            gm.customStart();
            p1.customStart();
            p2.customStart();
        }
    }


    
}
