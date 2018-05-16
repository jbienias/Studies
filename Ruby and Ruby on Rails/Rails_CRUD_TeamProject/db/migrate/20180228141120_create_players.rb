class CreatePlayers < ActiveRecord::Migration[5.2]
  def change
    create_table :players do |t|
      t.string :name
      t.string :surname
      t.string :nickname
      t.decimal :salary
      t.date :date_of_birth

      t.timestamps
    end
  end
end
