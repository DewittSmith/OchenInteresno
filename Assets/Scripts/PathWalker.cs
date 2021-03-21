using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PathWalker : MonoBehaviour
{
    [SerializeField] private LineBuilder line;
    [SerializeField] private Toggle loopToggle;
    [SerializeField] private TMP_InputField timeInput;

    [SerializeField] private float time = 1;
    private float t;
    private int i;

    private Path path;

    private void Start()
    {
        path = line.Path;
    }

    private void OnEnable()
    {
        timeInput.onEndEdit.AddListener(ChangeTime);
    }

    private void OnDisable()
    {
        timeInput.onEndEdit.RemoveListener(ChangeTime);
    }

    private void ChangeTime(string s)
    {
        if (float.TryParse(s, out float result))
        {
            time = result;
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            i = 0;
            t = 0;
        }

        float len = path.GetPathLength();
        if (len == 0) return;

        Vector2 newPos = path.Interpolate(ref t, ref i, loopToggle.isOn);
        transform.position = newPos;
        t += Time.deltaTime / path.GetSegmentLength(i) * len / time;
    }
}
