CREATE TABLE all_component_types(
  component_type_id INTEGER NOT NULL,
  component_type_name TEXT NOT NULL,
  type_table TEXT NOT NULL,
  PRIMARY KEY(component_type_id)
);

CREATE TABLE all_assemblages(
assemblage_id INTEGER NOT NULL, assemblage_name TEXT NOT NULL,
  PRIMARY KEY(assemblage_id)
);

CREATE TABLE all_entities
  (entity_id INTEGER NOT NULL, entity_name TEXT NOT NULL, PRIMARY KEY(entity_id))
  ;

CREATE TABLE assemblage_components(
  assemblage_id INTEGER NOT NULL,
  component_type_id INTEGER NOT NULL,
  PRIMARY KEY(assemblage_id, component_type_id),
  CONSTRAINT all_assemblages_assemblage_components
    FOREIGN KEY (assemblage_id) REFERENCES all_assemblages (assemblage_id)
      ON DELETE Cascade ON UPDATE Cascade,
  CONSTRAINT all_component_types_assemblage_components
    FOREIGN KEY (component_type_id)
      REFERENCES all_component_types (component_type_id) ON DELETE Cascade
      ON UPDATE Cascade
);

CREATE TABLE entity_components(
  component_data_id INTEGER NOT NULL,
  entity_id INTEGER NOT NULL,
  component_type_id INTEGER NOT NULL,
  PRIMARY KEY(component_data_id),
  CONSTRAINT all_entities_entity_components
    FOREIGN KEY (entity_id) REFERENCES all_entities (entity_id) ON DELETE Cascade
      ON UPDATE Cascade,
  CONSTRAINT all_component_types_entity_components
    FOREIGN KEY (component_type_id)
      REFERENCES all_component_types (component_type_id) ON DELETE Cascade
      ON UPDATE Cascade
);

CREATE TABLE xy_table(
  component_data_id INTEGER NOT NULL,
  "X" INTEGER NOT NULL,
  "Y" INTEGER NOT NULL,
  CONSTRAINT entity_components_xy_table
    FOREIGN KEY (component_data_id) REFERENCES entity_components (component_data_id)
      ON DELETE Cascade ON UPDATE Cascade
);

CREATE TABLE healthpoints_table(
component_data_id INTEGER NOT NULL, "Health" INTEGER NOT NULL,
  CONSTRAINT entity_components_healthpoints_table
    FOREIGN KEY (component_data_id) REFERENCES entity_components (component_data_id)
      ON DELETE Cascade ON UPDATE Cascade
);

CREATE TABLE grows_table(
component_data_id INTEGER NOT NULL, "isHarvestable" TEXT NOT NULL,
  CONSTRAINT entity_components_grows_table
    FOREIGN KEY (component_data_id) REFERENCES entity_components (component_data_id)
      ON DELETE Cascade ON UPDATE Cascade
);
