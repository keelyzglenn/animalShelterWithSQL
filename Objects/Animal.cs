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

    public Animal(string name, string gender, string date, string breed, int Id = 0)
    {
      _id = Id;
      _name = name;
      _gender = gender;
      _date = date;
      _breed = breed;
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
        return (idEquality && nameEquality && genderEquality && dateEquality && breedEquality);
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
        Animal newAnimal = new Animal(animalName, animalGender, animalDate, animalBreed, animalId);
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

      SqlCommand cmd = new SqlCommand("INSERT INTO animals (name, gender, date, breed) OUTPUT INSERTED.id VALUES (@AnimalName, @AnimalGender, @AnimalDate, @AnimalBreed);", conn);

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


      cmd.Parameters.Add(nameParameter);
      cmd.Parameters.Add(genderParameter);
      cmd.Parameters.Add(dateParameter);
      cmd.Parameters.Add(breedParameter);
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
      while(rdr.Read())
      {
        foundAnimalId = rdr.GetInt32(0);
        foundAnimalName = rdr.GetString(1);
        foundAnimalGender = rdr.GetString(2);
        foundAnimalDate = rdr.GetString(3);
        foundAnimalBreed = rdr.GetString(4);
      }
      Animal foundAnimal = new Animal(foundAnimalName, foundAnimalGender, foundAnimalDate, foundAnimalBreed, foundAnimalId);

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
