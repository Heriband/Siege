using System;
using System.Reflection;
using UnityEngine;

[System.Serializable]
public class Statistics
{
    [Header("Statistiques Offensives")]
    public float attackPower;        // Puissance d'attaque (dégâts infligés)
    public float attackSpeed;        // Vitesse d'attaque (délai entre chaque attaque)
    public float attackRange;        // Portée d'attaque (distance à laquelle l'unié peut attaquer)
    [Range(0f, 1f)]
    public float critChance;         // Chance de coup critique (%)
    public float critMultiplier;     // Dégâts d'un coup critique (multiplicateur)

    [Header("Statistiques Projectiles")]
    public float projectileNumber;   // Nombre de projectiles
    public float projectileSpeed;    // Vitesse des projectiles

    [Header("Statistiques Défensives")]
    public float health;             // Points de vie actuel
    public float healthMax;          // Points de vie total
    public float regen;              // R�génération de points de vie par seconde
    public float defense;            // R�duction des dégâts (par exemple, armure)
    [Range(0f, 1f)]
    public float blockChance;        // Chance de bloquer un coup (chance de ne pas recevoir de dégâts)
    public float shield;             // Valeur de bouclier (protection temporaire contre les dégâts)
    
    public float moveSpeed;          // Vitesse de déplacement

    [Header("Ressources")]
    public int gold;           

    public void Add(Statistics other)
    {
        foreach (FieldInfo field in typeof(Statistics).GetFields(BindingFlags.Public | BindingFlags.Instance))
        {
            float currentValue = (float) field.GetValue(this);
            float otherValue = (float) field.GetValue(other);
            field.SetValue(this, currentValue + otherValue);
        }

        health = Mathf.Min(health + other.health, healthMax);
    }

    public static Statistics operator +(Statistics a, Statistics b)
    {
        Statistics result = new Statistics();
        foreach (FieldInfo field in typeof(Statistics).GetFields(BindingFlags.Public | BindingFlags.Instance))
        {
            field.SetValue(result, (float)field.GetValue(a) + (float)field.GetValue(b));
        }

        result.health = Mathf.Min(result.health, result.healthMax);
        return result;
    }

    public new string ToString()
    {
        string output = "";
        foreach (FieldInfo field in typeof(Statistics).GetFields(BindingFlags.Public | BindingFlags.Instance))
        {
            output += field.Name + " : " + Math.Round((float)field.GetValue(this), 2)  + "\r\n";
        }
        return output;
    }
}
