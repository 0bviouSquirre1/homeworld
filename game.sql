CREATE TABLE all_components(
  "component_ID" INTEGER,
  official_name TEXT,
  description TEXT,
  table_name TEXT,
  PRIMARY KEY("component_ID"),
  UNIQUE(official_name),
  UNIQUE(table_name)
);

CREATE TABLE assemblage_components(
  assemblage_id INTEGER,
  component_id INTEGER,
  CONSTRAINT assemblages_assemblage_components
    FOREIGN KEY (assemblage_id) REFERENCES assemblages (assemblage_id),
  CONSTRAINT all_components_assemblage_components
    FOREIGN KEY (component_id) REFERENCES all_components ("component_ID")
);

CREATE TABLE assemblages
  (assemblage_id INTEGER NOT NULL, official_name TEXT, PRIMARY KEY(assemblage_id))
  ;

CREATE TABLE all_entities
  (entity_id INTEGER NOT NULL, human_label TEXT, PRIMARY KEY(entity_id));

CREATE TABLE entity_components(
  "component_ID" INTEGER,
  entity_id INTEGER NOT NULL,
  component_data_id INTEGER NOT NULL,
  PRIMARY KEY(component_data_id),
  CONSTRAINT all_entities_entity_components
    FOREIGN KEY (entity_id) REFERENCES all_entities (entity_id),
  CONSTRAINT all_components_entity_components
    FOREIGN KEY ("component_ID") REFERENCES all_components ("component_ID")
);

CREATE TABLE xy_table(
  component_data_id INTEGER,
  x INTEGER,
  y INTEGER,
  PRIMARY KEY(component_data_id)
);

CREATE TABLE healthpoints_table
  (component_data_id INTEGER, health INTEGER, PRIMARY KEY(component_data_id));

CREATE TABLE grows_table
  (component_data_id INTEGER, "isHarvestable" TEXT, PRIMARY KEY(component_data_id))
  ;
