json.extract! player, :id, :name, :surname, :nickname, :salary, :date_of_birth, :team_id # :created_at, :updated_at
json.url player_url(player, format: :json)
