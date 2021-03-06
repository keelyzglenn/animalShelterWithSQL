using System.Collections.Generic;
using System.Data.SqlClient;
using System;

namespace AnimalShelter
{
  public class Animal
  {
    private string _name;
    private string _gender;
    private string _date;
    private string _breed;
    private int _id;
    private int _typeId;

    public Animal(string name, string gender, string date, string breed, int typeId, int Id = 0)
    {
      _id = Id;
      _name = name;
      _gender = gender;
      _date = date;
      _breed = breed;
      _typeId = typeId;
    }

    public int GetTypeId()
    {
      return _typeId;
    }

    public void SetTypeId(int newTypeId)
    {
      _typeId = newTypeId;
    }

    public int GetId()
    {
      return _id;
    }

    public string GetName()
    {
      return _name;
    }
    public void SetName(string newName)
    {
      _name = newName;
    }

    public string GetGender()
    {
      return _gender;
    }
    public void SetGender(string newGender)
    {
      _gender = newGender;
    }

    public string GetDate()
    {
      return _date;
    }
    public void SetDate(string newDate)
    {
      _date = newDate;
    }

    public string GetBreed()
    {
      return _breed;
    }
    public void SetBreed(string newBreed)
    {
      _breed = newBreed;
    }

// orders by breed
    public static List<Animal> OrderByBreed()
    {
      List<Animal> allAnimals = new List<Animal>{};

      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM animals ORDER BY breed;", conn);
      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        int animalId = rdr.GetInt32(0);
        string animalName = rdr.GetString(1);
        string animalGender = rdr.GetString(2);
        string animalDate = rdr.GetString(3);
        string animalBreed = rdr.GetString(4);
        int animalTypeId = rdr.GetInt32(5);
        Animal newAnimal = new Animal(animalName, animalGender, animalDate, animalBreed, animalTypeId, animalId);
        allAnimals.Add(newAnimal);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return allAnimals;
    }
    // order by date
    public static List<Animal> OrderByDate()
    {
      List<Animal> allAnimals = new List<Animal>{};

      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM animals ORDER BY date;", conn);
      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        int animalId = rdr.GetInt32(0);
        string animalName = rdr.GetString(1);
        string animalGender = rdr.GetString(2);
        string animalDate = rdr.GetString(3);
        string animalBreed = rdr.GetString(4);
        int animalTypeId = rdr.GetInt32(5);
        Animal newAnimal = new Animal(animalName, animalGender, animalDate, animalBreed, animalTypeId, animalId);
        allAnimals.Add(newAnimal);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return allAnimals;
    }

    // allows database data to be compared to entered data
    public override bool Equals(System.Object otherAnimal)
    {
      if (!(otherAnimal is Animal))
      {
        return false;
      }
      else
      {
        Animal newAnimal = (Animal) otherAnimal;
        bool idEquality = this.GetId() == newAnimal.GetId();
        bool nameEquality = (this.GetName() == newAnimal.GetName());
        bool genderEquality = (this.GetGender() == newAnimal.GetGender());
        bool dateEquality = (this.GetDate() == newAnimal.GetDate());
        bool breedEquality = (this.GetBreed() == newAnimal.GetBreed());
        bool typeEquality = this.GetTypeId() == newAnimal.GetTypeId();
        return (idEquality && nameEquality && genderEquality && dateEquality && breedEquality && typeEquality);
      }
    }
    // get method to return all animals
    public static List<Animal> GetAll()
    {
      List<Animal> allAnimals = new List<Animal>{};

      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand ("SELECT * FROM animals;", conn);
      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        int animalId = rdr.GetInt32(0);
        string animalName = rdr.GetString(1);
        string animalGender = rdr.GetString(2);
        string animalDate = rdr.GetString(3);
        string animalBreed = rdr.GetString(4);
        int animalTypeId = rdr.GetInt32(5);
        Animal newAnimal = new Animal(animalName, animalGender, animalDate, animalBreed, animalTypeId, animalId);
        allAnimals.Add(newAnimal);
      }

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }

      return allAnimals;
    }
    // save method to save new object instances
    public void Save()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO animals (name, gender, date, breed, typeId) OUTPUT INSERTED.id VALUES (@AnimalName, @AnimalGender, @AnimalDate, @AnimalBreed, @AnimalTypeId);", conn);

      SqlParameter nameParameter = new SqlParameter();
      nameParameter.ParameterName = "@AnimalName";
      nameParameter.Value = this.GetName();

      SqlParameter genderParameter = new SqlParameter();
      genderParameter.ParameterName = "@AnimalGender";
      genderParameter.Value = this.GetGender();

      SqlParameter dateParameter = new SqlParameter();
      dateParameter.ParameterName = "@AnimalDate";
      dateParameter.Value = this.GetDate();

      SqlParameter breedParameter = new SqlParameter();
      breedParameter.ParameterName = "@AnimalBreed";
      breedParameter.Value = this.GetBreed();

      SqlParameter typeParameter = new SqlParameter();
      typeParameter.ParameterName = "@AnimalTypeId";
      typeParameter.Value = this.GetTypeId();


      cmd.Parameters.Add(nameParameter);
      cmd.Parameters.Add(genderParameter);
      cmd.Parameters.Add(dateParameter);
      cmd.Parameters.Add(breedParameter);
      cmd.Parameters.Add(typeParameter);
      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        this._id = rdr.GetInt32(0);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
    }
    // this will find animals based on IDs
    public static Animal Find(int id)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM animals WHERE id = @AnimalId;", conn);
      SqlParameter animalIdParameter = new SqlParameter();
      animalIdParameter.ParameterName = "@AnimalId";
      animalIdParameter.Value = id.ToString();
      cmd.Parameters.Add(animalIdParameter);
      SqlDataReader rdr = cmd.ExecuteReader();

      int foundAnimalId = 0;
      string foundAnimalName = null;
      string foundAnimalGender = null;
      string foundAnimalDate = null;
      string foundAnimalBreed = null;
      int foundTypeId = 0;
      while(rdr.Read())
      {
        foundAnimalId = rdr.GetInt32(0);
        foundAnimalName = rdr.GetString(1);
        foundAnimalGender = rdr.GetString(2);
        foundAnimalDate = rdr.GetString(3);
        foundAnimalBreed = rdr.GetString(4);
        foundTypeId = rdr.GetInt32(5);
      }
      Animal foundAnimal = new Animal(foundAnimalName, foundAnimalGender, foundAnimalDate, foundAnimalBreed, foundTypeId, foundAnimalId);

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }

      return foundAnimal;
    }

    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("DELETE FROM animals;", conn);
      cmd.ExecuteNonQuery();
      conn.Close();
    }
  }
}
