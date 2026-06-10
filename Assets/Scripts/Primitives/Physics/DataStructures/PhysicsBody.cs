namespace Primitives.Physics.DataStructures
{
    public struct PhysicsBody
    {
        public BodyType BodyType;
        public AABB Bounds;

        public PhysicsBody(AABB bounds, BodyType bodyType)
        {
            Bounds = bounds;
            BodyType = bodyType;
        }
    }
}