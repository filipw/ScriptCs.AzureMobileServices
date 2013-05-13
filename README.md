ScriptCs.AzureMobileServices
==============

Script pack for accessing Azure Mobile Services from scriptcs CSX script and scriptcs REPL.

## Installation

Simply type:

	scriptcs -install Scriptcs.AzureMobileServices

Or create packages.config:

	<?xml version="1.0" encoding="utf-8"?>
	<packages>
  		<package id="Scriptcs.AzureMobileServices" targetFramework="net45" />
	</packages>

And run:

	scriptcs -install
    
This will install `ScriptCs.AzureMobileServices` and the necessary dependencies and copy them to a `bin` folder relative to the place from where you executed the installation.

## Init

You can now access ZUMO from your script.

	var zumo = Require<AzureMobileServices>();
	var itemTable = zumo.GetTable<Item>("filipservice");

**Note**: due to the convention in Azure Mobile Services, the primary key column of your object needs to be a *nullable int* with the name "id" (or marked with *[JsonProperty("id")]* or *[DataMember(Name="id")]*). Otherwise the serialization will fail.

If you require an AUTH key, you can pass that too:

	var itemTable = zumo.GetTable<Item>("filipservice","myKey");

The name of the table is inferred from the class you are using. If that's different, you can pass that in as well:

	var itemTable = zumo.GetTable<Item>("filipservice","myKey","myTableName");

## Usage

Next you can start performing the typical CRUD operations on the table. Add and update operations will return the modified/created object.

### Add

	var item = itemTable.Add(new Item { Name = "Hello world" });
	//now item holds the id of the newly created object

### Update

	var item = new Item { Name = "Hello world" };
	itemTable.Update(1, item);
	//item with id == 1 has been updated

### Get

	var item = itemTable.Get(1);
	//gets item with id == 1

### Delete

	itemTable.Remove(1);
	//deletes item with id == 2

### Get all

	var items = itemTable.Get();
	//gets all items in the table


And that is it! You can combine this functionality with other script packs, or simply use in your scripts to easily persist/retrieve data and information.

This script pack works both from regular CSX script, and from the new scriptcs REPL.