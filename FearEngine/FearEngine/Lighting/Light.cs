using SharpDX;
public class Light : GameObject
{
    public Vector3 AmbientColor { get; set; }
    public Vector3 DiffuseColor { get; set; }
    public Vector3 Direction { get; set; }

    public Light()
    {
        AmbientColor = new Vector3(0.05f, 0.05f, 0.05f);
        DiffuseColor = new Vector3(1.0f, 1.0f, 1.0f);
        Direction = new Vector3(0.2f, -1.0f, 0.15f);
        Direction.Normalize();

        Transform.Position = new Vector3(0.0f, 5.0f, 0.0f);
    }
}