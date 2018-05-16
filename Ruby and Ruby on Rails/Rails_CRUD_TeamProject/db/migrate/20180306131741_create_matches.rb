class CreateMatches < ActiveRecord::Migration[5.2]
  def change
    create_table :matches do |t|
      t.integer :team_one_id
      t.integer :team_two_id
      t.string :result
      t.date :date

      t.timestamps
    end
  end
end
