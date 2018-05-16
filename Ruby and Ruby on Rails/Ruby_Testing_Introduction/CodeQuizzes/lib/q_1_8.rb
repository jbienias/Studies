class String
  def delete_last(n)
    n.times{chop!}
    self #ten sam obiekt, zmodyfikowany
  end
end

#def delete_last(num)
  #self[0...-num] #kopia obiektu ktora jest zmodyfikowana?
#end
