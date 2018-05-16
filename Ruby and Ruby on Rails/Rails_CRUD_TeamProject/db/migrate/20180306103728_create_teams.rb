class CreateTeams < ActiveRecord::Migration[5.2]
  def change
    create_table :teams do |t|
      t.string :name
      t.date :date_of_founding

      t.timestamps
    end
  end
end
