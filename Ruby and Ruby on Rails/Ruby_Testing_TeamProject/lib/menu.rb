# Implementation of Menu class
class Menu
  attr_accessor :menu

  def initialize
    @menu = "\n"
    @menu << "1. Wyswietl wszystkie gry \n"
    @menu << "2. Wyswietl wszystkich graczy \n"
    @menu << "3. Wyszukaj gre \n"
    @menu << "4. Wyszukaj gracza \n"
    @menu << "5. Dodaj gracza \n"
    @menu << "6. Usun gracza \n"
    @menu << "0. Zakoncz program \n"
    @menu << 'Wybierz numer by rozpoczac: -> '
  end

  def to_s
    menu
  end

  def clear
    system 'clear'
    system 'cls'
  end
end
