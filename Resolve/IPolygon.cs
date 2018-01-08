using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace Resolve
{
    /// <summary>
    ///  Represents a physical body.
    /// </summary>
    public interface IPolygon
    {
        Vector2 Origin { get; set; }
        List<Vector2> Points { get; }
        List<Vector2> Edges { get; }

        List<string> Tags { get; set; }
        object Data { get; set; }
        Action<IPolygon, IPolygon> OnCollide { get; }

        Vector2 Center { get; }
        bool IsTangible { get; set; }

        void Move(List<IPolygon> polygons, Vector2 vector);
        CollisionResult Simulate(IPolygon polygon, Vector2 vector);

        void AddTags(params string[] tags);
        void AddTag(string tag);
        void RemoveTags(params string[] tags);
        void RemoveTag(string tag);
        bool HasTag(string tag);

        void SetData(object data);
        object GetData();

        void SetCallback(Action<IPolygon, IPolygon> callback);

        void Draw(Action<Vector2, Vector2> drawLine, Action<string, Vector2> drawString);
        string ToString();
    }
}
