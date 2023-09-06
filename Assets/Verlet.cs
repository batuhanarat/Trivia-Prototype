using System.Collections.Generic;
using UnityEngine;

public class Verlet : MonoBehaviour
{
    private int rows = 4;
    private int columns = 4;
    private float spacing = 0.2f;
    public Material material;
    public GameObject hoop;
    public Transform[] pinPoints;
    private List<GameObject> spheres;
    private List<Particle> particles;
    private List<Connector> connectors;
    public GameObject basketball;
    public float ballRadius = 0.3f; // Radius of the basketball


    List<Vector2> transforms = new List<Vector2>();

    // Initalize
    void Start()
    {
        Vector2 spawnParticlePos = new Vector2(0, 5);

        spheres = new List<GameObject>();
        particles = new List<Particle>();
        connectors = new List<Connector>();

        for (int y = 0; y <= rows; y++)
        {
            for (int x = 0; x <= columns; x++)
            {
                bool isLastRow = particles.Count % (rows +1) != rows;
                bool isFirstRow = particles.Count % (rows + 1) != 1;
                // Create a sphere
                GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                sphere.GetComponent<MeshRenderer>().enabled = false;
                var mat = sphere.GetComponent<Renderer>();
                mat.material = material;

                sphere.transform.position = new Vector2(spawnParticlePos.y, spawnParticlePos.x);
                
                //sphere.transform.localScale = new Vector2(0.2f, 0.2f);
                transforms.Add(sphere.transform.position);
                // Create particle
                Particle point = new Particle();
                point.pinnedPos = new Vector2(spawnParticlePos.y, spawnParticlePos.x);

                // Create a vertical connector 
                if (x != 0)
                {
                    LineRenderer line = new GameObject("Line").AddComponent<LineRenderer>();

                    Connector connector = new Connector();
                    connector.p0 = sphere;
                    connector.p1 = spheres[spheres.Count - 1];

                    connector.point0 = point;
                    connector.point1 = particles[particles.Count - 1];
                    connector.point0.pos = sphere.transform.position;
                    connector.point0.oldPos = sphere.transform.position;

                    connectors.Add(connector);

                    connector.lineRender = line;
                    connector.lineRender.material = material;
                }

                // Create a horizontal connector
                if (y != 0 && isLastRow && x!=0)
                {
                    LineRenderer line = new GameObject("Line").AddComponent<LineRenderer>();

                    Connector connector = new Connector();
                    connector.p0 = sphere;
                    connector.p1 = spheres[(y - 1) * (rows + 1) + x];

                    connector.point0 = point;
                    connector.point1 = particles[(y - 1) * (rows + 1) + x];
                    connector.point0.pos = sphere.transform.position;
                    connector.point0.oldPos = sphere.transform.position;

                    connectors.Add(connector);

                    connector.lineRender = line;
                    connector.lineRender.material = material;

                }

                //Pin the points in the top row of the grid
                //if (x == 0)  // for trying bellow comment this if
                //{
                //    point.pinned = true;
                //}

                spawnParticlePos.x -= spacing;

                // Add particle and spehere to lists
                spheres.Add(sphere);
                particles.Add(point);
            }
            spawnParticlePos.x = 0;
            spawnParticlePos.y -= spacing;
        }
    }

    public class Connector
    {
        public bool enabled = true;
        public LineRenderer lineRender;
        public GameObject p0;
        public GameObject p1;
        public Particle point0;
        public Particle point1;
        public Vector2 changeDir;
    }

    public class Particle
    {
        public bool pinned = false;
        public Vector2 pinnedPos;
        public Vector2 pos;
        public Vector2 oldPos;
        public Vector2 vel;
        public float gravity = -0.24f;
        public float friction = 0.99f;
        public float damping = 1.2f;
    }

    void Update()
    {

        // I should move this to FixedUpdate but this works for now
        for (int i = 0; i < connectors.Count; i++)
        {
            float dist1 = Vector3.Distance(connectors[i].point0.pos, connectors[i].point1.pos);
            if (dist1 > 1.4)
            {
                connectors[i].enabled = false;
            }
        }
    }
    bool initialAttachComplete = false;
    float lerpSpeed = 0.05f;  

    private void FixedUpdate()
    {
        if (!initialAttachComplete)
        {
            bool allPointsClose = true;

            for (int x = 0; x <= columns; x++)
            {
                int index = x * (rows + 1);
                Particle point = particles[index];
                Vector2 targetPosition = pinPoints[x].transform.position;

                if (Vector2.Distance(point.pinnedPos, targetPosition) > 2)
                {
                    allPointsClose = false;
                    break;
                }
            }
            if (allPointsClose)
            {
                initialAttachComplete = true;
            }

            // Smoothly move toward the new hoop positions
            for (int x = 0; x <= columns; x++)
            {
                int index = x * (rows + 1); 
                Particle point = particles[index];
                Vector2 targetPosition = pinPoints[x].transform.position;

                point.pinnedPos = Vector2.Lerp(point.pinnedPos, targetPosition, lerpSpeed);
                point.pos = point.pinnedPos;
                point.oldPos = point.pinnedPos;
            }
        }
        else
        {
            // Directly set to the new hoop positions
            for (int x = 0; x <= columns; x++)
            {
                int index = x * (rows + 1);  // index in your spheres and particles list
                Particle point = particles[index];

                point.pinnedPos = pinPoints[x].transform.position;
                point.pos = point.pinnedPos;
                point.oldPos = point.pinnedPos;
            }
        }
        // Update particle positions
        for (int p = 0; p < particles.Count; p++)
        {
            Particle point = particles[p];
            if (point.pinned == true)
            {
                point.pos = point.pinnedPos;
                point.oldPos = point.pinnedPos;
            }
            else
            {
                point.vel = (point.pos - point.oldPos) * point.friction;
                point.vel *= (1f - point.damping * Time.fixedDeltaTime);  // Damping term
                point.oldPos = point.pos;

                point.pos += point.vel;
                point.pos.y += point.gravity * Time.fixedDeltaTime;
            }
        }
         
        // Constraint the points together
        var startDistance = 0.2f;
        for (int i = 0; i < connectors.Count; i++)
        {
            if (connectors[i].enabled == false)
            {
                // Do nothing
            }

            else
            {
                float dist = (connectors[i].point0.pos - connectors[i].point1.pos).magnitude;
                float error = Mathf.Abs(dist - startDistance);

                if (dist > startDistance)
                {
                    connectors[i].changeDir = (connectors[i].point0.pos - connectors[i].point1.pos).normalized;
                }
                else if (dist < startDistance)
                {
                    connectors[i].changeDir = (connectors[i].point1.pos - connectors[i].point0.pos).normalized;
                }
                Vector2 changeAmount = connectors[i].changeDir * error;

                connectors[i].point0.pos -= changeAmount * startDistance;
                connectors[i].point1.pos += changeAmount * startDistance;

            }
        }
        foreach (Particle point in particles)
        {
            if (point.pinned)
                continue;

            Vector2 toBall = point.pos - (Vector2)basketball.transform.position;
            float distanceToBall = toBall.magnitude;

            // Check for collision
            if (distanceToBall < ballRadius)
            {
                // Collision response
                Vector2 correction = toBall.normalized * (ballRadius - distanceToBall);
                point.pos += correction;
            }
        }
        // Set spheres
        for (int p = 0; p < particles.Count; p++)
        {
            Particle point = particles[p];
            spheres[p].transform.position = new Vector2(point.pos.x, point.pos.y);
            spheres[p].transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);

        }

        // Set lines
        for (int i = 0; i < connectors.Count; i++)
        {
            if (connectors[i].enabled == false)
            {
                Destroy(connectors[i].lineRender);
            }

            else
            {
                // Set points for the lines
                var points = new Vector3[2];
                points[0] = connectors[i].p0.transform.position + new Vector3(0, 0, -1);
                points[1] = connectors[i].p1.transform.position + new Vector3(0, 0, -1);

                // Draw lines
                connectors[i].lineRender.startWidth = 0.04f;
                connectors[i].lineRender.endWidth = 0.04f;
                connectors[i].lineRender.SetPositions(points);

            }

        }
    }
}