using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LineGenerator : MonoBehaviour
{
    [SerializeField] private CommandManager commandManager;
    [SerializeField] private GameObject linePrefab;
    [SerializeField] private Slider slider;
    [SerializeField] private Color color;

    private Line _currentLine;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.K))
        {
            commandManager.Undo();
        }
        
        if (Input.GetMouseButtonDown(0) && !IsPointerOverUIObject())
        {
            ICommand createLineCommand = new CreateLineCommand(linePrefab, slider.value, color);
            commandManager.ExecuteCommand(createLineCommand);
            CreateLineCommand createLine = (CreateLineCommand)createLineCommand;
            _currentLine = createLine.Line;
            // CreateLine(slider.value, color);
        }
        
        if(Input.GetMouseButtonUp(0))
        {
            _currentLine = null;
        }

        if (_currentLine != null)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            _currentLine.UpdateLine(mousePosition);
        }
    }

    private void CreateLine(float size, Color color)
    {
        _currentLine = Instantiate(linePrefab, Vector3.zero, Quaternion.identity).GetComponent<Line>();
        _currentLine.SetupLine(size, color);
    }
    
    public static bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }
}
