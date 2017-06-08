using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipSystems : MonoBehaviour {

    public Transform testHardpoint;

    public Transform ship;
    public int shipID; // Identifies the ship in the save data 
    public int shipClassID; // Identifies what type of ship it is in the game data database

    public List<Equipment> equipment = new List<Equipment>();
    public List<PowerUser> powerUsers = new List<PowerUser>();

    public List<Generator> generators = new List<Generator>();
    public List<Thruster> thrusters = new List<Thruster>();
    public List<Shield> shields = new List<Shield>();
    public List<Weapon> weapons = new List<Weapon>();

    public float shipMass = 10; // Ship mass in tonns

    // Use this for initialization
    void Start () {
        addTempEquip();
        // Decompile the equipment list into power users and individual
        foreach(Equipment equipPiece in equipment)
        {
            distributeEquip(equipPiece);
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey("g"))
        {
            instanceNewComponent();
        }

        foreach(Weapon weapon in weapons)
        {
            weapon.UpdateComponent();
        }
    }

    // Find the remaining power
    public int get_PowerRemaining()
    {
        int totalPowerOutput = 0;
        foreach(Generator generator in generators)
        {
            totalPowerOutput = totalPowerOutput + generator.get_powerOutput();
        }
        int currentPowerUsage = 0;
        foreach(PowerUser powerUser in powerUsers)
        {
            currentPowerUsage = currentPowerUsage + powerUser.get_PowerUsageCurrent();
        }
        return totalPowerOutput - currentPowerUsage;
    }

   // public int get_

    private void addTempEquip()
    {
        equipment.Add(new Shield { name = "Test Shield", powerUsageMaximum = 3 });
        equipment.Add(new Thruster { name = "Test Thruster", powerUsageMaximum = 3, accelerationAtMaxPower = 10.01f, maxSpeedAtMaxPower = 10000.1f, turnSpeedAtMaxPower = 0.1f });
        equipment.Add(new Generator { name = "Test Generator",  powerOutputMax = 5});
        equipment.Add(new Generator { name = "Test Generator", powerOutputMax = 5});
        equipment.Add(new BeamWeapon { name = "Test Beam Weapon", powerUsageMaximum = 3 });
        equipment.Add(new BeamWeapon { name = "Test Beam Weapon", powerUsageMaximum = 3 });
        equipment.Add(new BeamWeapon { name = "Test Beam Weapon", powerUsageMaximum = 3 });
        equipment.Add(new BeamWeapon { name = "Test Beam Weapon", powerUsageMaximum = 3 });
        equipment.Add(new BeamWeapon { name = "Test Beam Weapon", powerUsageMaximum = 3 });
        equipment.Add(new MissileWeapon { name = "Test Missile Weapon", powerUsageMaximum = 3 });
        equipment.Add(new Thruster { name = "Test Thruster 2" , powerUsageMaximum = 3, accelerationAtMaxPower = 0.01f, maxSpeedAtMaxPower = 0.1f, turnSpeedAtMaxPower = 0.1f });
        equipment.Add(new Thruster { name = "Test Thruster 3", powerUsageMaximum = 3, accelerationAtMaxPower = 0.01f, maxSpeedAtMaxPower = 0.1f, turnSpeedAtMaxPower = 0.1f });
    }

    public int updatePowerValue(int setTo, PowerUser setItem)
    {
        int powerRemaining = get_PowerRemaining();
        if (powerRemaining >= (setTo - setItem.get_PowerUsageCurrent()))
        {
            setItem.set_PowerUsageCurrent(setTo);
        }
        else
        {
            setItem.set_PowerUsageCurrent(setItem.get_PowerUsageCurrent() + powerRemaining);
        }
        return setItem.get_PowerUsageCurrent();
    }

    public float get_currentMaxSpeed()
    {
        float speedFromThrusters = 0;
        foreach(Thruster thruster in thrusters)
        {
            speedFromThrusters = speedFromThrusters + thruster.get_maxSpeedCurrent();
        }
        return speedFromThrusters * shipMass;
    }

    public float get_currentAcceleration()
    {
        float acelFromThrusters = 0;
        foreach (Thruster thruster in thrusters)
        {
            acelFromThrusters = acelFromThrusters + thruster.get_accelerationCurrent();
        }
        return acelFromThrusters * shipMass;
    }

    public float get_currentTurnSpeed()
    {
        float turnFromThrusters = 0;
        foreach (Thruster thruster in thrusters)
        {
            turnFromThrusters = turnFromThrusters + thruster.get_turnSpeedCurrent();
        }
        return turnFromThrusters * shipMass;
    }

    public void instanceNewComponent()
    {
        equipment.Add(new Generator { name = "Test Generator", powerOutputMax = 5 });
        distributeEquip(equipment[equipment.Count - 1]);
        equipment.Add(new BeamWeapon { equipmentID = 1, size = 1, acceptedHardPointTypes = { "Thruster", "Univseral" }, name = "Test Weapon", powerUsageMaximum = 3});
        equipment[equipment.Count - 1].set_hardPoint(testHardpoint);
        distributeEquip(equipment[equipment.Count - 1]);
    }

    public void instanceSavedComponent()
    {

    }
    
    // Place an equipment piece into the appriopriate lists
    public void distributeEquip(Equipment equipPiece)
    {
        if (equipPiece is PowerUser)
        {
            powerUsers.Add((PowerUser)equipPiece);
            if (equipPiece is Thruster)
            {
                thrusters.Add((Thruster)equipPiece);
            }
            else if (equipPiece is Shield)
            {
                shields.Add((Shield)equipPiece);
            }
            else if (equipPiece is Weapon)
            {
                weapons.Add((Weapon)equipPiece);
            }
        }

        else if (equipPiece is Generator)
        {
            generators.Add((Generator)equipPiece);
        }
    }
}
