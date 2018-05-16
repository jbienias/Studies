class Array
  def mode
    #http://ruby-doc.org/core-2.4.2/Enumerable.html
    #about inject
    freq = inject(Hash.new(0)) do |m,v| # {} does not work
      m[v] += 1
      m
    end
    sort do |x, y|
      freq[x] <=> freq[y]
    end.last
  end
end
