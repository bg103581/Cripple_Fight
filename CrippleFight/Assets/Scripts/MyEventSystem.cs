using UnityEngine.EventSystems;
using UnityEngine;

public class MyEventSystem : EventSystem
{

    public void Start() {
    }
    protected override void OnEnable()
    {
        base.OnEnable();
    }

    protected override void Update()
    {
        EventSystem originalCurrent = EventSystem.current;
        current = this;
        base.Update();
        current = originalCurrent;

        if (EventSystem.current.currentSelectedGameObject == null) {
            EventSystem.current.SetSelectedGameObject(firstSelectedGameObject);
        }
    }
}