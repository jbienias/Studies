json.extract! player, :id, :name, :surname, :nickname, :salary, :date_of_birth # :created_at, :updated_at
json.url player_url(player, format: :json)
