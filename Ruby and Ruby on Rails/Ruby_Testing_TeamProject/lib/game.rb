# Implementation of Game class
class Game
  attr_accessor :name, :id, :year_of_release, :type

  def initialize(name, id, year_of_release, type)
    @name = name
    @id = id
    @year_of_release = year_of_release
    @type = type
    @users = []
  end

  def add_user(user)
    @users.push(user)
  end

  def users_get
    @users
  end

  def to2_s
    game = 'Nazwa: ' << name.to_s << "\n"
    game << 'Rok Wydania: ' << year_of_release.to_s << "\n"
    game << 'Typ : ' << type.to_s << "\n"
    game << 'Gracze: '
    @users.each do |a|
      game << a.login << ', '
    end
    game << "\n\n"
  end
end
