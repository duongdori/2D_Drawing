using UnityEngine;

public class CreateLineCommand : ICommand
{
    private GameObject _linePrefab;
    private float _size;
    private Color _color;
    public Line Line { get; private set; } = null;
    
    public CreateLineCommand(GameObject linePrefab, float size, Color color)
    {
        _linePrefab = linePrefab;
        _size = size;
        _color = color;
    }

    public void Execute()
    {
        Line = Object.Instantiate(_linePrefab, Vector3.zero, Quaternion.identity).GetComponent<Line>();
        Line.SetupLine(_size, _color);
    }

    public void Undo()
    {
        if (Line != null)
        {
            Object.Destroy(Line.gameObject);
        }
    }
}