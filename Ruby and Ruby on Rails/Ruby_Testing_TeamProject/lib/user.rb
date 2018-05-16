# Implementation of User class
require 'csv'
require 'fileutils'

class User
  attr_accessor :login, :password, :date_of_birth, :token, :id

  def initialize(login, password, date_of_birth, token, id)
    @login = login
    @password = password
    @date_of_birth = date_of_birth
    @token = token
    @id = id
  end

  def to_s
    user = 'Nick: ' << login.to_s << "\n"
    user << 'Rok urodzenia: ' << date_of_birth.to_s << "\n"
    user << 'Token: ' << token.to_s << "\n"
    user << 'Wiek: '
    user << (2018 - date_of_birth.to_s.scan(/\d+/).first.to_i).to_s << "\n"
  end

  def add_user(path = '../lib/csv/users.csv')
    if !dob_valid?(@date_of_birth)
      print 'Data urodzenia niepoprawna'
    else
      line = @login.to_s + ';' + @password.to_s + ';'
      line = line + @date_of_birth.to_s + ';' + @token.to_s + ';' + id.to_s + "\n"
      File.open(path, 'a') { |file| file.write(line) }
      print 'Dodano uzytkownika ' + @login
    end
  end

  def delete_user(tmp = '../lib/csv/tmp.csv', path = '../lib/csv/users.csv')
    f = File.open(tmp, 'w')
    write_data(f, path)
    f.close
    FileUtils.mv(tmp, path)
    print 'Uzytkownik usuniety'
  end

  def write_data(f, path)
    File.foreach(path) do |line|
      index = line.split(';').last.to_i
      f.puts line unless index == @id.to_i
    end
  end

  def find_user(id, users)
    index = nil
    users.each do |user|
      index = users.index(user) unless user.id != id
    end
    index
  end

  def dob_valid?(date_of_birth)
    y, m, d = date_of_birth.split '-'
    Date.valid_date? y.to_i, m.to_i, d.to_i
  end
end
