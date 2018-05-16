def start_with_c?(str)
  str.split.all? do |f| #split('') nie dziala
    f.start_with? "c"
  end
end
