# Implementation of ToArray 'monkey patch' class
require_relative 'game'
require_relative 'user'
require_relative 'menu'
require 'csv'

class ToArray
  def users_to_a(path = '../lib/csv/users.csv')
    correct_file(path)
    array = []
    users = CSV.read(path, col_sep: ';')
    users.shift
    users.each do |row|
      login, password, date_of_birth, token, id = row
      a = User.new(login, password, date_of_birth, token, id)
      array.push(a)
    end
    array
  end

  def games_to_a(auth, path = '../lib/csv/games.csv')
    correct_file(path)
    array = []
    games = CSV.read(path, col_sep: ';')
    games.shift
    games.each do |row|
      name, id, year_of_release, type, users = row
      users_id = users.split(',')
      b = Game.new(name, id, year_of_release, type)
      users_id.each do |a|
        b.add_user(auth.at(a.to_i))
      end
      array.push(b)
    end
    array
  end

  def correct_file(path)
    s = File.read(path)
    s.encode(s.encoding, :universal_newline => true)
    File.write(path, s)
  end
end
