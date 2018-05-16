require_relative 'game'
require_relative 'user'
require_relative 'menu'
require_relative 'to_a'
require_relative 'print'

read = ToArray.new
print = Print.new
menu = Menu.new

users = read.users_to_a
games = read.games_to_a(users)

key = 1

while key == 1
  puts menu.to_s
  case gets.chop
  when '1'
    menu.clear
    print.games_to_s(games)
    gets.chop
    menu.clear
  when '2'
    menu.clear
    print.users_to_s(users)
    gets.chop
    menu.clear
  when '3'
    menu.clear
    print 'Wyszukaj: '
    input = gets.chop
    while input.length < 3 || input == 'x'
      break if input == 'x'
      print 'Podaj przynajmniej trzy znaki (x - anulowanie wyszukania)'
      input = gets.chop
    end
    found = 0
    games.each do |b|
      if b.name.include?(input)
        puts b.to2_s
        found = 1
      end
    end
    puts 'Nie znaleziono zadnej gry spelniajacej kryteria.' if found != 1
    gets.chop
    menu.clear
  when '4'
    menu.clear
    print 'Wyszukaj: '
    input = gets.chop
    while input.length < 3 || input == 'x'
      break if input == 'x'
      print 'Podaj przynajmniej trzy znaki (x - anulowanie wyszukania)'
      input = gets.chop
    end
    found = 0
    users.each do |a|
      if a.login.include?(input) || a.password.include?(input)
        puts a.to_s
        found = 1
      end
    end
    puts 'Nie znaleziono zadnego gracza spelniajacego kryteria.' if found != 1
    gets.chop
    menu.clear
  when '5'
    menu.clear
    print 'Podaj login: '
    login = gets.chop
    print 'Podaj haslo: '
    pass = gets.chop
    print 'Podaj date urodzenia: '
    dob = gets.chop
    print 'Podaj token: '
    token = gets.chop
    max = users.size
    id = 0
    id += 1 while id < max && !users[id].nil?
    user = User.new(login, pass, dob, token, id)
    user.add_user
    users = read.users_to_a
    gets.chop
    menu.clear
  when '6'
    menu.clear
    print 'Podaj id uzytkownika: '
    id = gets.chop
    user_index = users[0].find_user(id, users)
    unless user_index.nil?
      users[user_index].delete_user
      users = read.users_to_a
    end
    gets.chop
    menu.clear
  when '0'
    puts 'Nastepuje zamkniecie programu'
    key = 0
    break
  else
    menu.clear
    puts 'Podales zla liczbe! Sprobuj jeszcze raz!'
  end
end
