using System.Collections.Generic;
namespace Hw.CodeBuilder.Modle
{
    public class EntityInitProperties
    {
    }

    public class Default
    {
        public string db { get; set; }
        public string dbConn { get; set; }
        public List<Field> entityInitFields { get; set; }
        public EntityInitProperties entityInitProperties { get; set; }
    }

    public class Sql
    {
        public string delimiter { get; set; }
    }

    public class CodeTemplate
    {
        public string type { get; set; }
        public bool isDefault { get; set; }
        public string applyFor { get; set; }
        public string createTable { get; set; }
        public string createIndex { get; set; }
        public string content { get; set; }
    }

    public class GeneratorDoc
    {
        public string docTemplate { get; set; }
    }

    public class UiHint
    {
        public string defKey { get; set; }
        public string defName { get; set; }
    }

    public class Profile
    {
        public Default @default { get; set; }
        public string javaHome { get; set; }
        public Sql sql { get; set; }
        public List<string> dataTypeSupports { get; set; }
        public List<CodeTemplate> codeTemplates { get; set; }
        public GeneratorDoc generatorDoc { get; set; }
        public string relationFieldSize { get; set; }
        public List<UiHint> uiHint { get; set; }
    }

    public class Properties
    {
    }

    public class Header
    {
        public bool freeze { get; set; }
        public string refKey { get; set; }
        public bool hideInGraph { get; set; }
    }

    public class Field
    {
        public string defKey { get; set; }
        public string defName { get; set; }
        public string comment { get; set; }
        public string type { get; set; }
        public string len { get; set; }
        public string scale { get; set; }
        public bool primaryKey { get; set; }
        public bool notNull { get; set; }
        public bool autoIncrement { get; set; }
        public string defaultValue { get; set; }
        public bool hideInGraph { get; set; }
        public string domain { get; set; }
        public bool dataBaseField { get; set; }
        public string refDict { get; set; }
        public bool? dtoAdd { get; set; }
        public bool? dtoUpdate { get; set; }
        public bool? dtoList { get; set; }
        public bool? dtoSearch { get; set; }
        public bool? parentId { get; set; }
        public string uiHint { get; set; }
    }

    public class Correlation
    {
        public string myField { get; set; }
        public string refEntity { get; set; }
        public string refField { get; set; }
        public string myRows { get; set; }
        public string refRows { get; set; }
        public string innerType { get; set; }
    }

    public class Entity
    {
        public string defKey { get; set; }
        public string defName { get; set; }
        public string comment { get; set; }
        public Properties properties { get; set; }
        public string nameTemplate { get; set; }
        public List<Header> headers { get; set; }
        public List<Field> fields { get; set; }
        public List<Correlation> correlations { get; set; }
        public List<object> indexes { get; set; }
    }

    public class Item
    {
        public string defKey { get; set; }
        public string defName { get; set; }
        public string sort { get; set; }
        public string parentKey { get; set; }
        public string intro { get; set; }
        public bool enabled { get; set; }
        public string attr1 { get; set; }
        public string attr2 { get; set; }
        public string attr3 { get; set; }
        public string __key { get; set; }
    }

    public class Dict
    {
        public string defKey { get; set; }
        public string defName { get; set; }
        public string sort { get; set; }
        public string intro { get; set; }
        public List<Item> items { get; set; }
    }

    public class Mapping
    {
        public string defKey { get; set; }
        public string defName { get; set; }
        public string JAVA { get; set; }
        public string ORACLE { get; set; }
        public string MYSQL { get; set; }
        public string SQLServer { get; set; }
        public string PostgreSQL { get; set; }
        public string DB2 { get; set; }
        public string DM { get; set; }

    }

    public class DataTypeMapping
    {
        public string referURL { get; set; }
        public List<Mapping> mappings { get; set; }
    }

    public class Domain
    {
        public string defKey { get; set; }
        public string defName { get; set; }
        public string applyFor { get; set; }
        public object len { get; set; }
        public object scale { get; set; }
        public string uiHint { get; set; }
    }

    public class Source
    {
        public string cell { get; set; }
        public string port { get; set; }
    }

    public class Target
    {
        public string cell { get; set; }
        public string port { get; set; }
    }

    public class Position
    {
        public double x { get; set; }
        public int y { get; set; }
    }

    public class Cell
    {
        public string id { get; set; }
        public string shape { get; set; }
        public Source source { get; set; }
        public Target target { get; set; }
        public string relation { get; set; }
        public string fillColor { get; set; }
        public Position position { get; set; }
        public int? count { get; set; }
        public string originKey { get; set; }
    }

    public class CanvasData
    {
        public List<Cell> cells { get; set; }
    }

    public class Diagram
    {
        public string defKey { get; set; }
        public string defName { get; set; }
        public CanvasData canvasData { get; set; }
    }

    public class CodeModel
    {
        public string name { get; set; }
        public string describe { get; set; }
        public string avatar { get; set; }
        public string version { get; set; }
        public string createdTime { get; set; }
        public string updatedTime { get; set; }
        public List<object> dbConns { get; set; }
        public Profile profile { get; set; }
        public List<Entity> entities { get; set; }
        public List<object> views { get; set; }
        public List<Dict> dicts { get; set; }
        public List<object> viewGroups { get; set; }
        public DataTypeMapping dataTypeMapping { get; set; }
        public List<Domain> domains { get; set; }
        public List<object> standardFields { get; set; }
        public List<Diagram> diagrams { get; set; }
    }
}