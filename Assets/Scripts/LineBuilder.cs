using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;
using System.Net;

[RequireComponent(typeof(LineRenderer))]
public class LineBuilder : MonoBehaviour
{
    [SerializeField] private float simplifyTolerance = 1;
    [SerializeField] private Button generateButton;
    [SerializeField] private TMP_InputField countInput;
    [SerializeField] private TMP_InputField timeInput;
    [SerializeField] private Toggle closeToggle;

    private LineRenderer lineRenderer;
    private ILineGenerator lineGenerator = new RandomLineGenerator();

    public Path Path { get; } = new CurvePath();

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();

        string json = Resources.Load<TextAsset>("path").text;
        using (WebClient wc = new WebClient())
            json = wc.DownloadString("https://vk.com/doc125444443_592467118?hash=d6ec6069eac7101f2a&dl=b0ca94051945f86705");

        PointsData pd = JsonUtility.FromJson<PointsData>(json);

        closeToggle.isOn = pd.Loop;
        timeInput.text = pd.PathTime.ToString();
        countInput.text = pd.Points.Length.ToString();

        SetLine(pd.Points);
    }

    private void OnEnable()
    {
        generateButton.onClick.AddListener(Generate);
        closeToggle.onValueChanged.AddListener(Close);
    }

    private void OnDisable()
    {
        generateButton.onClick.RemoveListener(Generate);
        closeToggle.onValueChanged.RemoveListener(Close);
    }

    private void Close(bool isClosed)
    {
        SetLine(Path.Nodes);
    }

    private void Generate()
    {
        Rect visibleRect = new Rect();
        visibleRect.min = Camera.main.ViewportToWorldPoint(Vector2.zero);
        visibleRect.max = Camera.main.ViewportToWorldPoint(Vector2.one);

        Vector3[] nodes = lineGenerator.Generate(int.Parse(countInput.text), visibleRect, false);
        SetLine(nodes);
    }

    private void SetLine(Vector3[] nodes)
    {
        Path.UpdateNodes(nodes, closeToggle.isOn);

        lineRenderer.positionCount = Path.Points.Length;
        lineRenderer.SetPositions(Path.Points);
        lineRenderer.Simplify(simplifyTolerance);
    }
}
