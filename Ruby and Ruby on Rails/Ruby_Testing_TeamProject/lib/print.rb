# Implementation of Print class
require_relative 'game'
require_relative 'user'

class Print
  def users_to_s(users)
    users.each do |a|
      puts a.to_s
    end
  end

  def games_to_s(games)
    games.each do |b|
      puts b.to2_s
    end
  end
end
