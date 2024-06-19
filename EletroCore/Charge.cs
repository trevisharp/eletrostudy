using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;

namespace EletroCore;

public class Charge(int eletrons, float x0, float y0)
{
    public const float ElementaryCharge = 1.60218E-19f;
    public const float ElementaryMass = 9.10938E-31f;
    public const float CoulombConstant = 8.98751E+9f;

    public float X { get; private set; } = x0;
    public float Y { get; private set; } = y0;
    public float Dx { get; private set; } = 0f;
    public float Dy { get; private set; } = 0f;
    public readonly float Mass = ElementaryMass;
    public readonly float Coulomb = -eletrons * ElementaryCharge;

    public static void Move(IEnumerable<Charge> charges, float dt)
    {
        foreach (var charge in charges)
        {
            charge.X += charge.Dx * dt;
            charge.Y += charge.Dy * dt;
        }
    }

    public static void Apply(IEnumerable<Charge> charges, float dt)
    {
        var k = CoulombConstant;
        var array = charges.ToArray();
        for (int i = 0; i < array.Length; i++)
        {
            var c1 = array[i];
            var f0 = k * c1.Coulomb; 
            for (int j = i + 1; j < array.Length; j++)
            {
                var c2 = array[j];

                var dx = c1.X - c2.X;
                var dy = c1.Y - c2.Y;
                var r = dx * dx + dy * dy;

                var mod = MathF.Sqrt(r);
                var ux = dx / mod;
                var uy = dy / mod;

                var f = f0 * c2.Coulomb / r;

                c1.Dx += ux * f * dt / c1.Mass;
                c1.Dy += uy * f * dt / c1.Mass;
                
                c2.Dx += ux * f * dt / c2.Mass;
                c2.Dy += uy * f * dt / c2.Mass;
            }
        }
    }
}